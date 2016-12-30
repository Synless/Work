#include <Keyboard.h>;

void setup()
{
  SerialUSB.begin(115200);
  pinMode(0,INPUT);
  Keyboard.begin();
}

void loop() 
{
  int counter = 0;
  bool test = false;
  t = micros();
  while(micros()-t<1000000)
  {
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
      if(n%30==0)
      {
        Keyboard.release('a');  
      }
      
    }
    counter++;
  }
  SerialUSB.print("counter = ");  SerialUSB.println(counter);
  delay(1000);
}

//ABOUT 1000HZ IF "1/30 OF THE POLLS RAISE AN EVENT"
