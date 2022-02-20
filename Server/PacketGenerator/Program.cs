﻿
using System.Xml;

namespace PacketGenerator
{
    class program
    {
        static string genPackets;
        static ushort packetId;
        static string packetEnums;
       // static string managerRegister;
        static string serverRegister;
        static string clientRegister;
        static void Main(string[] args)
        {
            string pdlPath = "../../../PDL.xml";

            XmlReaderSettings settings = new XmlReaderSettings()
            {
                IgnoreComments = true,
                IgnoreWhitespace = true
            };

            if (args.Length >= 1)
                pdlPath = args[0];

            using (XmlReader r = XmlReader.Create(pdlPath, settings))
            {
                r.MoveToContent();
                while (r.Read())
                {
                    if (r.Depth == 1 && r.NodeType == XmlNodeType.Element)
                        ParsePacket(r);

                    // Console.WriteLine(r.Name+" "+r["name"]);
                }

                string fileText= string.Format(PacketFormat.fileFormat, packetEnums, genPackets);
                File.WriteAllText("GenPackets.cs", fileText);
               // string managerText = string.Format(PacketFormat.managerFormat, managerRegister);
                string clientManagerText = string.Format(PacketFormat.managerFormat, clientRegister);
                File.WriteAllText("ClientPacketManager.cs", clientManagerText);
                string serverManagerText = string.Format(PacketFormat.managerFormat, serverRegister);
                File.WriteAllText("ServerPacketManager.cs", serverManagerText);
            }

        }
        public static void ParsePacket(XmlReader r)
        {
            if (r.NodeType == XmlNodeType.EndElement)
                return;
            if (r.Name.ToLower() != "packet")
            {
                Console.WriteLine("invalid packet node");

                return;
            }

            string packetName = r["name"];
            if (string.IsNullOrEmpty(packetName))
            {
                Console.WriteLine("Packet without name");
                return;
            }
            Tuple<string, string, string>  t =ParseMembers(r);
            genPackets += string.Format(PacketFormat.packetFormat,
               packetName, t.Item1, t.Item2, t.Item3);

            packetEnums += string.Format(PacketFormat.PacketEnumFormat, packetName, ++packetId);

            if(packetName.StartsWith("S2C_") || packetName.StartsWith("s2c_"))
                clientRegister += string.Format(PacketFormat.managerRegisterFormat, packetName);
            if (packetName.StartsWith("C2S_") || packetName.StartsWith("c2s_"))
                serverRegister += string.Format(PacketFormat.managerRegisterFormat, packetName);

            //managerRegister += string.Format(PacketFormat.managerRegisterFormat, packetName);
        }

        //{1} 멤버 변수들
        //{2} 멤버 변수 Read
        //{2} 멤버 변수 Write

        public static Tuple<string,string,string> ParseMembers(XmlReader r)
        {
            string packetName = r["name"];

            string memberCode = "";
            string readCode = "";
            string writeCode = "";

            int depth = r.Depth + 1;

            while (r.Read())
            {
                if (r.Depth != depth)
                    break;

                string memberName = r["name"];
                if (string.IsNullOrEmpty(memberName))
                {
                    Console.WriteLine("Member without name");
                    return null;
                }

                if(string.IsNullOrEmpty(memberCode)==false)
                {
                    memberCode += Environment.NewLine;
                }
                if (string.IsNullOrEmpty(readCode) == false)
                {
                    readCode += Environment.NewLine;
                }
                if (string.IsNullOrEmpty(writeCode) == false)
                {
                    writeCode += Environment.NewLine;
                }
                string memeberType = r.Name.ToLower();
                switch (memeberType)
                {
                    case "byte":
                    case "sbyte":
                        memberCode += string.Format(PacketFormat.memberFormat, memeberType, memberName);
                        readCode += string.Format(PacketFormat.readByteFormat, memberName, memeberType);
                        writeCode += string.Format(PacketFormat.writeByteFormat, memberName, memeberType);
                        break;
                    case "bool":
                    case "short":
                    case "ushort":
                    case "int":
                    case "long":
                    case "float":
                    case "double":
                        memberCode += string.Format(PacketFormat.memberFormat, memeberType, memberName);
                        readCode += string.Format(PacketFormat.readFormat, memberName, ToMemberType(memeberType), memeberType);
                        writeCode += string.Format(PacketFormat.writeFormat, memberName, memeberType);
                        break;
                    case "string":
                        memberCode += string.Format(PacketFormat.memberFormat, memeberType, memberName);
                        readCode += string.Format(PacketFormat.readStringFormat, memberName);
                        writeCode += string.Format(PacketFormat.writeStringFormat, memberName);
                        break;
                    case "list":
                        Tuple<string,string,string> t=   ParseList(r);
                        memberCode += t.Item1;
                        readCode += t.Item2;
                        writeCode += t.Item3;
                        break;
                    default:
                        break;
                }

            }

            memberCode = memberCode.Replace("\n", "\n\t");
            readCode = readCode.Replace("\n", "\n\t");
            writeCode = writeCode.Replace("\n", "\n\t");
            return new Tuple<string, string, string>(memberCode, readCode, writeCode);

        }

        public static string ToMemberType(string memberType)
        {
            switch(memberType)
            {
                case "bool":
                    return "ToBoolean";
                //case "byte":
                case "short":
                    return "ToInt16";
                case "ushort":
                    return "ToInt16";
                case "int":
                    return "ToInt32";
                case "long":
                    return "ToInt64";
                case "float":
                    return "ToSingle";
                case "double":
                    return "ToDouble";
                default:
                    return "";
            }
        }
        public static Tuple<string, string, string> ParseList(XmlReader r)
        {
            string listName = r["name"];
            if(string.IsNullOrEmpty(listName))
            {
                Console.WriteLine("List without name");
                return null;
            }

            Tuple<string, string, string>  t =ParseMembers(r);
            string memberCode = string.Format(PacketFormat.memberListFormat,
                FirstCharToUpper(listName),
                FirstCharToLower(listName),
                t.Item1,
                t.Item2,
                t.Item3
                );

            string readCode = string.Format(PacketFormat.readListFormat,
                 FirstCharToUpper(listName),
                FirstCharToLower(listName)
                );

            string writeCode = string.Format(PacketFormat.writeListFormat,
                       FirstCharToUpper(listName),
                     FirstCharToLower(listName)
                 );

            return new Tuple<string, string, string>(memberCode,readCode,writeCode);
        }


        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            return input[0].ToString().ToUpper() + input.Substring(1);
        }
        public static string FirstCharToLower(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            return input[0].ToString().ToLower() + input.Substring(1);
        }

    }
}