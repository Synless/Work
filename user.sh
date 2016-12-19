#!/bin/bash

RED='\e[31m';

BLUE='\e[34m';

YEL='\e[33m';

NC='\e[0m';



USER="dummyUser"

PASSWD="passwd"

MAIL="dum@dum.com"



#Check if both parameters are pressent ...

if [ "$#" -ne 2 ]; then

	echo -e "${RED}Error${NC} -> Missing parameters";

	echo -e "${RED}Exiting${NC}";

	exit 1;

fi

#... and if they are correct

if [ ! -f "$1" ]; then

	echo -e "${RED}Error${NC} -> file $1 does not exist";

	echo -e "${RED}Exiting${NC}";

	exit 1;

fi

if [ `grep -c "$2" /etc/group` ]; then

	echo 

else

	sudo groupadd "$2";

fi



cat "$1" | while read LINE

do

	USER=`echo "$LINE" | cut -d ';' -f1 | tr -d "\'"`;

	PASSWD=`echo "$LINE" | cut -d ';' -f2 | tr -d "\'"`;

	MAIL=`echo "$LINE" | cut -d ';' -f3 | tr -d "\'"`;

	echo -e "_______________________________\n";

	echo -e "${YEL}Deleting user : ${BLUE}$USER${NC}";

	sudo userdel "$USER" 2>/dev/null;

	echo -e "${YEL}Creating new user : ${BLUE}$USER${NC}";

	sudo useradd -m -g "$2" -c "$MAIL" -p `openssl passwd -1 "$PASSWD"` "$USER" 2>/dev/null;

done

echo -e "\nDone\n";