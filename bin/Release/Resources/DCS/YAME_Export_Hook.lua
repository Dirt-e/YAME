---------------------------------------------------------------------------------------------------
-- Data export script for YAME
-- Version 0.02
-- created by @Dirty, find me here: https://www.xsimulator.net/community/members/dirty.27556/

--
-- Changes:
-- Added 'omega_dot'
---------------------------------------------------------------------------------------------------
Version = "v0.02"
Simulator = "DCS"

--Initialise variables:
Counter = 0
Time = 0
PrevTime = 0
DeltaTime= 0 
omega_lon_prev = 0
omega_vrt_prev = 0
omega_lat_prev = 0
omega_lon_dot = 0
omega_vrt_dot = 0
omega_lat_dot = 0

--Don't change theses values!!!
r2d = 57.295779513082323		--converts Rad 	into degrees 
m2k = 1.94384					--converts m/s 	into knots
g2mpss = 9.806					--converts G	into m/s^2

--Change those accordigng to your preferences
YouPreferDegreesOverRadians = true
YouPreferKnotsOverMetersPerSecond = false
YouPreferMetersPerSecondSquaredOverG = true

--The callbacks to be registered with DCS
local YAME_Callbacks = {}

function YAME_Callbacks.onSimulationStart()

	log.write('YAME', log.INFO, "Starting motion data export to YAME. Export script " .. Version)
	
	package.path = package.path..";.\\LuaSocket\\?.lua"
	package.cpath = package.cpath..";.\\LuaSocket\\?.dll"
	socket = require("socket")

	IP = "127.0.0.1"
	Port = 31090
	MySocket = socket.udp ( )
	MySocket:settimeout ( 0 )
	MySocket:setpeername ( IP, Port )
	
	
end

function YAME_Callbacks.onSimulationFrame()
	
	--Metadata:
	PrevTime	= Time
	Time		= Export.LoGetModelTime()
	DeltaTime	= Time - PrevTime
	Counter = Counter + 1
	
	--Airdata:
	IAS = 				Export.LoGetIndicatedAirSpeed()
	Machnumber = 		Export.LoGetMachNumber()
	TAS = 				Export.LoGetTrueAirSpeed()
	vv = 				Export.LoGetVectorVelocity()
	GS = 					math.sqrt( (vv.x * vv.x) + (vv.z * vv.z) )
	AOA = 				Export.LoGetAngleOfAttack()
	VerticalSpeed = 	Export.LoGetVerticalVelocity()
	Height = 			Export.LoGetAltitudeAboveGroundLevel()
		
	if (YouPreferKnotsOverMetersPerSecond) then
		IAS = IAS * m2k
	end
		
	--Euler Angles:
	pitch, bank, yaw = Export.LoGetADIPitchBankYaw()
	
	if (YouPreferDegreesOverRadians) then
		yaw 	= yaw 	* r2d
		pitch 	= pitch * r2d
		bank 	= bank 	* r2d
	end

	--Angular rates:
	omega = Export.LoGetAngularVelocity()				--Radians!
		omega_lon = omega.x
		omega_vrt = omega.y
		omega_lat = omega.z
		
	if (YouPreferDegreesOverRadians) then
		omega_lon = omega_lon	* r2d
		omega_vrt = omega_vrt	* r2d
		omega_lat = omega_lat	* r2d
	end
	
	--Angular accelerations:	
	if (DeltaTime > 0)	then									
		omega_lon_dot = (omega_lon - omega_lon_prev)	/ DeltaTime	
		omega_vrt_dot = (omega_vrt - omega_vrt_prev)	/ DeltaTime	
		omega_lat_dot = (omega_lat - omega_lat_prev)	/ DeltaTime	
	end		
		omega_lon_prev = omega_lon;								
		omega_vrt_prev = omega_vrt;								
		omega_lat_prev = omega_lat;	
		
		
	--Accelerations:
	accel = Export.LoGetAccelerationUnits()
		accel_lon	= accel.x
		accel_vrt	= accel.y
		accel_lat	= accel.z
		
	if (YouPreferMetersPerSecondSquaredOverG) then
		accel_lon	= accel_lon	*	g2mpss
		accel_vrt	= accel_vrt	*	g2mpss
		accel_lat	= accel_lat	*	g2mpss
	end	
		
	
		
	
	----------------- EXPORT SLOTS ----------------
	--Airdata:
	local slot_00 = IAS
	local slot_01 = Machnumber
	local slot_02 = TAS
	local slot_03 = GS
	local slot_04 = AOA
	local slot_05 = VerticalSpeed
	local slot_06 = Height
	--Euler:
	local slot_07 = bank
	local slot_08 = yaw
	local slot_09 = pitch
	--Inertial Data:
	local slot_10 = omega_lon
	local slot_11 = omega_vrt
	local slot_12 = omega_lat
	local slot_13 = omega_lon_dot
	local slot_14 = omega_vrt_dot
	local slot_15 = omega_lat_dot
	local slot_16 = accel_lon
	local slot_17 = accel_vrt
	local slot_18 = accel_lat
	local slot_19 = Time
	local slot_20 = DeltaTime
	local slot_21 = Counter
	local slot_22 = Simulator
	
	--Send data via UDP
	if MySocket then
	--								Slots:      [0]   [1]   [2]   [3]   [4]   [5]   [6]   [7]   [8]   [9]   [10]  [11]  [12]  [13]  [14]  [15]  [16]  [17]  [18]  [19]  [20]  [21]  [22] 
		socket.try(MySocket:send(string.format("%.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %s, \n", 
												slot_00,
												slot_01,
												slot_02,
												slot_03,
												slot_04,
												slot_05,
												slot_06,
												slot_07,
												slot_08,
												slot_09,
												slot_10,
												slot_11,
												slot_12,
												slot_13,
												slot_14,
												slot_15,
												slot_16,
												slot_17,
												slot_18,
												slot_19,
												slot_20,
												slot_21,
												slot_22
												)))
	end
end

function YAME_Callbacks.onSimulationStop()
	
	log.write('YAME', log.INFO, "Motion data export stopped")

	if MySocket then
		MySocket:close()
	end
end

--register these callbacks with DCS to be called at the appropriate moment.
DCS.setUserCallbacks(YAME_Callbacks)




------------------------------------------------------------------------------------
-----------------------------   HELPER FUNCTIONS   ---------------------------------
------------------------------------------------------------------------------------

--NIL
