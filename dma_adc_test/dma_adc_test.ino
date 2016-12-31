#include "Joystick.h";

Joystick joystick;

void setup()
{
  SerialUSB.begin(115200);
  while(!SerialUSB)
  {
    
  }
}

void loop() 
{
  SerialUSB.println("Looping");
  SerialUSB.print("A0 : ");SerialUSB.println(joystick.getX());
  SerialUSB.print("A1 : ");SerialUSB.println(joystick.getY());
  delay(50);
}
