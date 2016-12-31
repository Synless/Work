#include <Keyboard.h>;

long t = 0;
long counter = 0;
bool test=false;

void setup()
{  
  SerialUSB.begin();
  Keyboard.begin();
  pinMode(0,INPUT);
}

void loop() 
{  
  if(micros()-t>1000000)
  {    
    SerialUSB.print("counter = ");  SerialUSB.println(counter);
    counter=0;
    delay(1000);    
    t=micros();  
  }
  for(int n = 0; n < 30; n++)
  {
    if(test && digitalRead(0)==LOW)
    {
      test=false;
    }
    else if(!test && digitalRead(0)==HIGH);
    {
      test=true;
    }
    if(n%10==0)
    {
      Keyboard.release('a');  
    }    
  }
  counter++;
}
