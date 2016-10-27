void setup() 
{
    Serial.begin(115200);
    while(!Serial) { }
    
    char input[100] = "A bird came down the walk";
    char *token = strtok(input, " ");
    while (token != NULL) 
    {
        Serial.println(token);
        token = strtok(NULL, " ");
    }
}

void loop() 
{  
}
