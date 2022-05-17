using System.Collections.Generic;
using System;
using System.Collections;
using Newtonsoft.Json;


[Serializable]
public class jsonRoom
{
    public string id;
    public string type;
    public location Location;

    [Serializable]
    public class location
    {
        public string type;
        public Value value;
        public double[][][] coordinates;
    }

    [Serializable]
    public class Value
    {
        public string type;
        public double[][][] coordinates;
    }


}