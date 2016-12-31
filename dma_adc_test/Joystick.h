#include <Arduino.h>

class Joystick
{
  private:
    int x;
    int y;
   
  public:
   Joystick(); 
   int getX();
   int getY();
};
