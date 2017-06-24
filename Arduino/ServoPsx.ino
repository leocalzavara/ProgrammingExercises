////////////////////////////////////////////////////////////////////////////////////////////
// Programa para controlar braco robotico de 6 servo motores com um controle de Playstation.
// Autores:
// Leonardo P. Calzavara
// Marcel B. Lopes
////////////////////////////////////////////////////////////////////////////////////////////

#include <Psx.h>
#include <Servo.h>

#define PSX_DATA_PIN 8
#define PSX_CMD_PIN 4
#define PSX_ATT_PIN 12
#define PSX_CLOCK_PIN 7

#define LED_PIN 13

#define INCREMENTO 10
#define MIN_POS 0
#define MAX_POS 180
#define DELAY_MOVIMENTO 20

Psx Psx; // Initializes the library

int tecla = 0;

short posServo1 = 90, posServo2 = 90, posServo3 = 90, posServo4 = 90, posServo5 = 90, posServo6 = 90;

Servo servo1;
Servo servo2;
Servo servo3;
Servo servo4;
Servo servo5;
Servo servo6;

void setup()
{
  Psx.setupPins(PSX_DATA_PIN, PSX_CMD_PIN, PSX_ATT_PIN, PSX_CLOCK_PIN, 10);  // Defines what each pin is used
  // (Data Pin #, Cmnd Pin #, Att Pin #, Clk Pin #, Delay)
  // Delay measures how long the clock remains at each state, measured in microseconds.
  // too small delay may not work (under 5)
  
  pinMode(LED_PIN, OUTPUT); // Establishes LED_PIN as an output so the LED can be seen

  servo1.attach(3);
  servo2.attach(5);
  servo3.attach(6);
  servo4.attach(9);
  servo5.attach(10);
  servo6.attach(11);

  Serial.begin(9600);

  // Move os servos para as posicoes iniciais
  servo1.write(posServo1);
  servo2.write(posServo2);
  servo3.write(posServo3);
  servo4.write(posServo4);
  servo5.write(posServo5);
  servo6.write(posServo6);
}

void incrementaServo(Servo& servo, short& posicaoAtual)
{
  short novaPosicao = posicaoAtual + INCREMENTO;
  if(novaPosicao > MAX_POS)
  {
    novaPosicao = MAX_POS;
  }
  
  for (short i = posicaoAtual; i <= novaPosicao; i++)
  {
    servo.write(i);
    delay(DELAY_MOVIMENTO);
  }
  
  posicaoAtual = novaPosicao;
}

void decrementaServo(Servo& servo, short& posicaoAtual)
{
  short novaPosicao = posicaoAtual - INCREMENTO;
  if(novaPosicao < MIN_POS)
  {
    novaPosicao = MIN_POS;
  }
  
  for (short i = posicaoAtual; i >= novaPosicao; i--)
  {
    servo.write(i);
    delay(DELAY_MOVIMENTO);
  }
  
  posicaoAtual = novaPosicao;
}

void loop()
{
  tecla = Psx.read(); // Psx.read() initiates the PSX controller and returns

  Serial.println();
  Serial.println();
  Serial.println();
  Serial.println();
  Serial.println();
  Serial.println();
   // Serial.print("tecla = ");  Serial.println(tecla);
  Serial.print("Servo 1: "); Serial.println(posServo1);
  Serial.print("Servo 2: "); Serial.println(posServo2);
  Serial.print("Servo 3: "); Serial.println(posServo3);
  Serial.print("Servo 4: "); Serial.println(posServo4);
  Serial.print("Servo 5: "); Serial.println(posServo5);
  Serial.print("Servo 6: "); Serial.println(posServo6);
  Serial.println();
  Serial.println();
  Serial.println();
  Serial.println();
  Serial.println();

  if (tecla & psxR2) // If the data anded with a button's hex value is true,
  {
    digitalWrite(LED_PIN, HIGH); // If button is pressed, turn on the LED
  }
  else
  {
    digitalWrite(LED_PIN, LOW); // If the button isn't pressed, turn off the LED
  }

  if (tecla & psxX)
  {
    incrementaServo(servo1, posServo1);
  }

  if (tecla & psxTri)
  {
    decrementaServo(servo1, posServo1);
  }

  if (tecla & psxSqu)
  {
    incrementaServo(servo2, posServo2);
  }

  if (tecla & psxO)
  {
    decrementaServo(servo2, posServo2);
  }

  if (tecla & psxDown)
  {
    incrementaServo(servo3, posServo3);
  }

  if (tecla & psxUp)
  {
    decrementaServo(servo3, posServo3);
  }

  if (tecla & psxLeft)
  {
    incrementaServo(servo4, posServo4);
  }

  if (tecla & psxRight)
  {
    decrementaServo(servo4, posServo4);
  }

  if (tecla & psxL1)
  {
    incrementaServo(servo5, posServo5);
  }

  if (tecla & psxL2)
  {
    decrementaServo(servo5, posServo5);
  }

  if (tecla & psxSlct)
  {
    incrementaServo(servo6, posServo6);
  }

  if (tecla & psxStrt)
  {
    decrementaServo(servo6, posServo6);
  }

  delay(100);
}