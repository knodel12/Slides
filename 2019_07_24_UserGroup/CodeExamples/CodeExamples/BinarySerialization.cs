using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeExamples
{
    class BinarySerialization
    {
        public static void RunSerializationTests()
        {
            File.Delete("MySerializableTypeTest.txt");
            File.Delete("MySerializableTypeWithCustomDeserializationTest.txt");
            using (FileStream stream = File.Create("MySerializableTypeTest.txt"))
            {
                var myTest = new MySerializableType("First Test");
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, myTest);
                Console.WriteLine("First Test Before Serialization");
                Console.WriteLine(myTest);
            }

            using (var stream = File.OpenRead("MySerializableTypeTest.txt"))
            {
                var formatter = new BinaryFormatter();
                var v = (MySerializableType)formatter.Deserialize(stream);
                Console.WriteLine("First Test After Serialization");
                Console.WriteLine(v);
            }

            using (FileStream stream = File.Create("MySerializableTypeWithCustomDeserializationTest.txt"))
            {
                var myTest = new MySerializableTypeWithCustomDeserialization("Second Test");
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, myTest);
                Console.WriteLine("Second Test Before Serialization");
                Console.WriteLine(myTest);
            }

            using (var stream = File.OpenRead("MySerializableTypeWithCustomDeserializationTest.txt"))
            {
                var formatter = new BinaryFormatter();
                var v = (MySerializableTypeWithCustomDeserialization)formatter.Deserialize(stream);
                Console.WriteLine("Second Test After Serialization");
                Console.WriteLine(v);
            }


            // JSON
            var serializableObject = new SerializableOnly("Some Random Thing");
            var jsonObject = new MySerializableType("Some Random Thing");
            Console.WriteLine($"Serializable attribute only: {JsonConvert.SerializeObject(serializableObject)}");
            Console.WriteLine($"Serializable and JsonObject attribute: {JsonConvert.SerializeObject(jsonObject)}");
        }

        [Serializable]
        class SerializableOnly
        {
            public SerializableOnly(string ignoredValue)
            {
                Console.WriteLine($"{nameof(SerializableOnly)} ignoredValue constructor called!");
                _ignoredField = ignoredValue;
                Prop = ignoredValue.ToUpper();
                _field = ignoredValue.ToLower();
            }

            public string Prop { get; set; }

            protected string _field;

            [NonSerialized]
            protected string _ignoredField;
        }

        [Serializable]
        class MySerializableTypeWithCustomDeserialization
            : MySerializableType, ISerializable
        {
            public MySerializableTypeWithCustomDeserialization(string ignoredValue)
                : base(ignoredValue)
            {
                Console.WriteLine($"{nameof(MySerializableTypeWithCustomDeserialization)} ignoredValue constructor called!");
            }

            private MySerializableTypeWithCustomDeserialization(SerializationInfo info, StreamingContext context)
                : base("")
            {
                Console.WriteLine($"{nameof(MySerializableTypeWithCustomDeserialization)} SerializationInfo constructor called!");
                foreach (var item in info)
                {
                    switch (item.Name)
                    {
                        case nameof(Prop):
                            Prop = (string)item.Value;
                            break;
                        case nameof(_ignoredField):
                            _ignoredField = (string)item.Value;
                            break;
                        case nameof(_field):
                            _field = (string)item.Value;
                            break;
                    }
                }
            }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue(nameof(_ignoredField), _ignoredField);
                info.AddValue(nameof(_field), _field);
                info.AddValue(nameof(Prop), Prop);
            }
        }

        [JsonObject]
        [Serializable]
        class MySerializableType
        {
            public MySerializableType()
            {
                Console.WriteLine($"{nameof(MySerializableType)} empty constructor called!");
            }

            public MySerializableType(string ignoredValue)
            {
                Console.WriteLine($"{nameof(MySerializableType)} ignoredValue constructor called!");
                _ignoredField = ignoredValue;
                Prop = ignoredValue.ToUpper();
                _field = ignoredValue.ToLower();
            }

            public string Prop { get; set; }

            protected string _field;

            [NonSerialized]
            protected string _ignoredField;

            public override string ToString()
            {
                return $@"
{nameof(Prop)}: {Prop}
{nameof(_field)}: {_field}
{nameof(_ignoredField)}: {_ignoredField}
";
            }
        }
        // Define other methods and classes here

    }
}
