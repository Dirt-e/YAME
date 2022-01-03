---------------------------------------------------------------------------------------------------
-- Data export script for YAME
-- Version 0.01
-- created by @Dirty, find me here: https://www.xsimulator.net/community/members/dirty.27556/

--
-- Changes:
-- Initial version
---------------------------------------------------------------------------------------------------
Version = "v0.01"
Simulator = "DCS"

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
	
	Counter = 0
end

function YAME_Callbacks.onSimulationFrame()

	--Airdata:
	IAS = 				Export.LoGetIndicatedAirSpeed()
	Machnumber = 		Export.LoGetMachNumber()
	TAS = 				Export.LoGetTrueAirSpeed()
	vv = 				Export.LoGetVectorVelocity()
	GS = 					math.sqrt( math.pow(vv.x,2) + math.pow(vv.z,2))
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
	omega = Export.LoGetAngularVelocity()
		omega_longitudinal	= omega.x
		omega_vertical		= omega.y
		omega_lateral		= omega.z
		
	if (YouPreferDegreesOverRadians) then
		omega_longitudinal	= omega_longitudinal	* r2d
		omega_vertical		= omega_vertical		* r2d
		omega_lateral		= omega_lateral			* r2d
	end
		
	--Accelerations:
	accel = Export.LoGetAccelerationUnits()
		accel_longitudinal	= accel.x
		accel_vertical		= accel.y
		accel_lateral		= accel.z
		
	if (YouPreferMetersPerSecondSquaredOverG) then
		accel_longitudinal	= accel_longitudinal	*	g2mpss
		accel_vertical		= accel_vertical		*	g2mpss
		accel_lateral		= accel_lateral			*	g2mpss
	end	
		
	--Metadata:
	Time = Export.LoGetModelTime()
	Counter = Counter + 1
		
	
	--put data to be exported into these slots. You can put any data you like in there :-)
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
	local slot_10 = omega_longitudinal
	local slot_11 = omega_vertical
	local slot_12 = omega_lateral
	local slot_13 = accel_longitudinal
	local slot_14 = accel_vertical
	local slot_15 = accel_lateral
	local slot_16 = Time
	local slot_17 = Counter
	local slot_18 = Simulator
	
	--Send data via UDP
	if MySocket then
	--								Slots:      [0]   [1]   [2]   [3]   [4]   [5]   [6]   [7]   [8]   [9]   [10]  [11]  [12]  [13]  [14]  [15]  [16]  [17]  [18] 
		socket.try(MySocket:send(string.format("%.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %.7f, %s, \n", 
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
												slot_18
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
