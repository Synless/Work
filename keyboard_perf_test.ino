#include <Keyboard.h>;

long t = 0;
int counter = 0;
bool test=false;
long mean = 0;
double tri = 0.0;

void setup()
{
  pinMode(0,INPUT);
  Keyboard.begin();
}

void loop() 
{
  t=micros();  
  for(int n = 0; n < 35; n++)
  {
    if(test && digitalRead(0)==LOW)
    {
      test=false;
    }
    else if(!test && digitalRead(0)==HIGH);
    {
      test=true;
    }
    if(n%25==0)
    {
      Keyboard.releaseAll();  
    }    
  }
  counter++;
  mean += micros()-t;
  tri = (double)(mean)/counter;
  tri /= 1000;
  SerialUSB.print("Time to process : "); SerialUSB.print(tri);
  SerialUSB.println(" ms");
  delay(100);
}
