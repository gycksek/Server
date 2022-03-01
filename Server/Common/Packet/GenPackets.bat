start ../../PacketGenerator/bin/Debug/net6.0/PacketGenerator.exe ../../PacketGenerator/PDL.xml
xcopy /y GenPackets.cs "../../DummyClient"
xcopy /y GenPackets.cs "../../Server"
xcopy /y GenPackets.cs "../../../../test/test/Assets/Scripts/DummyClient"

xcopy /y ClientPacketManager.cs "../../DummyClient"
xcopy /y ClientPacketManager.cs "../../../../test/test/Assets/Scripts/DummyClient"
xcopy /y ServerPacketManager.cs "../../Server"