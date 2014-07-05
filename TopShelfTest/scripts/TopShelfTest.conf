# TopShelf Example WebService

description "TopShelfTest"  
author      "Mostly ServiceStack Honestly"

start on started rc  
stop on stopping rc

respawn

exec start-stop-daemon --start -c vagrant --exec /usr/bin/mono /vagrant/TopShelfTest/TopShelfTest.exe  