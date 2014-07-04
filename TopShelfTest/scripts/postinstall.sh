#!/bin/bash

ln -s /opt/TopShelfTest/scripts/TopShelfTest.conf /etc/init/TopShelfTest.conf  
initctl reload-configuration
start TopShelfTest  