Welcome to YAME :-)

an open source motion cueing software for 6DOF Stewart-type motion plarforms using linear actutors.


Overview:
YAME (btw an acronym for Y_et A_nother M_otion E_ngnine) is a WPF application that does not follow the MVVM principle rigorously. 
To start out, take a look at "engine.cs" There's a function called "StartEngine()". This function spins up another thread through a BackgroundWorker. 
