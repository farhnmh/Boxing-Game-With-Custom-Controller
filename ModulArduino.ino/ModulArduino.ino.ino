#include<Wire.h>
const int MPU=0x68;
int16_t GyX;
int PB = 13;

unsigned long prevMillis = 0;
int msDelay = 10;

void setup(){
  Serial.begin(19200);
  
  Wire.begin();
  Wire.beginTransmission(MPU);
  Wire.write(0x6B);
  Wire.write(0);
  Wire.endTransmission(true);

  pinMode(PB, INPUT_PULLUP);
}

void loop(){
  unsigned long currentMillis = millis();
  if(currentMillis - prevMillis > msDelay){
    inputLoop();
    prevMillis = currentMillis;
  }
}

void inputLoop(){
  Wire.beginTransmission(MPU);
  Wire.write(0x3B);
  Wire.endTransmission(false);
  Wire.requestFrom(MPU,12,true);
  GyX=Wire.read()<<8|Wire.read();

  int PB_state = digitalRead(PB);
  
  //idle
  if (-7500 < GyX && GyX < 7500)
    GyX = 0;
    
  //range pertama
  if (-7500 > GyX)
    GyX = 1;
  if (7500 < GyX)
    GyX = -1;
    
  Serial.print(GyX);
  Serial.print(",");
  Serial.println(!PB_state);
}
