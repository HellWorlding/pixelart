using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace pixart
{
    [Serializable]
    public class PaintPacket
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int ColorNumber { get; set; }

        public static byte[] Serialize(PaintPacket packet)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, packet);
                return ms.ToArray();
            }
        }

        // 역직렬화 (byte[] → 객체)
        public static PaintPacket Deserialize(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (PaintPacket)bf.Deserialize(ms);
            }
        }
    }

    [Serializable]
    public class PatternPacket
    {
        public Color[,] BlockColors { get; set; }
        public int[,] ColorNumbers { get; set; }
        public Dictionary<Color, int> ColorMap { get; set; }
        public int BlockSize { get; set; }
        public int ColorCount { get; set; }

        public static byte[] Serialize(PatternPacket packet)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, packet);
                return ms.ToArray();
            }
        }

        public static PatternPacket Deserialize(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (PatternPacket)bf.Deserialize(ms);
            }
        }
    }

    /// <summary>
    /// [신규] '전체 색칠' 액션을 알리기 위한 신호용 패킷입니다.
    /// (NEW: Signal packet to notify of a 'Color All' action.)
    /// </summary>
    [Serializable]
    public class ColorAllPacket
    {
        // 이 패킷은 신호용이므로 내용이 필요 없습니다.
        // (This packet is just a signal, so it doesn't need any properties.)

        public static byte[] Serialize(ColorAllPacket packet)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, packet);
                return ms.ToArray();
            }
        }

        public static ColorAllPacket Deserialize(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (ColorAllPacket)bf.Deserialize(ms);
            }
        }
    }
}