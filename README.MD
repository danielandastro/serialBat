serialBat (and yes that is camelCase) is a simple application for transmitting current battery data over a serial port. The main use of the program is to deliver battery data to a microcontroller, for example an arduino, to allow an external battery display or alarm.
The config files (.ini) in the config folder correspond to certain aspects of the serial and battery data:
seridev: serial device (COM3, COM4 etc)

serrate: serial baud rate (9600, usually)

serint: interval between data transmissions (in milliseconds)

pwrstat: whether to send powerline status (before percentage sends 1 if online, 0 if not)

battden: the way to denote battery data (true or 100 sends percent, outof3 sends data range 1-3 rounded to nearest integer, outof5 sends data range 1-5 rounded to nearest integer, and outof10 sends data range 1-10 rounded to nearst integer

The config folder must be kept in the same directory as the executable, and a sample config folder has been provided, containing these values:
seridev.ini: COM3
serrate.ini: 9600
serint.ini: 5000
pwrstat.ini: false
battden.ini: true
