using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO.Compression;
using LocalCommons.Network;

namespace LocalCommons.Cryptography
{
    class DispatcherCompress
    {



//    private static Inflater inflater3 = new Inflater(true);
//    @Override
//    public CryptResult decrypt(byte[] raw, packetDirection dir)
//    {
//        if (dir == packetDirection.serverPacket && raw[1] == (byte)0x04)
//        {
//            Inflater inflater = new Inflater(true);
//            int size = raw[2];
//            byte[] potentialInit = Arrays.copyOfRange(raw, 4, raw.length);
//            byte[] buffer = new byte[32768];
//            ByteArrayOutputStream outputStream = new ByteArrayOutputStream(raw.length);
//            outputStream.write(raw[0]);
//            try
//            {
//                outputStream.write(new byte[] { 1 });
//            }
//            catch (IOException ex)
//            {
//                Logger.getLogger(ArcheageGameCBTCrypter.class.getName()).log(Level.SEVERE, null, ex);
//       }
//       int len = potentialInit.length;
//       int off = 0;
//            while (!inflater.finished()) {
//                try {

//                    if (inflater.needsInput()) {
//                        int part;

//                        if (len< 1) {
//                            break;
//                        }
//                        //part = (len < 84 ? len : 84);
//                        part = len;
//                        inflater.setInput(potentialInit, off, part);
//                        off += part;
//                        len -= part;
//                   }
//                    int count = inflater.inflate(buffer);
//                    outputStream.write(buffer, 0, count);
//                } catch (DataFormatException ex) {
//                    System.out.println("Packet decompression error: "+ex.getMessage());
//                    return new CryptResult(raw, size);
//                }
//            }
//            try {
//                outputStream.close();
//            } catch (IOException ex) {
//            }
//            System.out.println(size);
//            return new CryptResult(outputStream.toByteArray(), size);
//        }
//        else if (dir == packetDirection.serverPacket && raw[1] == (byte) 0x03) {
//            byte[] potentialInit = Arrays.copyOfRange(raw, 2, raw.length);
//            byte[] buffer = new byte[32768];
//            ByteArrayOutputStream outputStream = new ByteArrayOutputStream(raw.length);
//            outputStream.write(raw[0]);
//            try {
//                outputStream.write(new byte[]{1});
//            } catch (IOException ex) {
//                Logger.getLogger(ArcheageGameCBTCrypter.class.getName()).log(Level.SEVERE, null, ex);
//            }
//            int len = potentialInit.length;
//            int off = 0;
//            while (!inflater3.finished()) {
//                try {

//                    if (inflater3.needsInput()) {
//                        int part;

//                        if (len< 1) {
//                            break;
//                        }
//                        //part = (len < 84 ? len : 84);
//                        part = len;
//                        inflater3.setInput(potentialInit, off, part);
//                        off += part;
//                        len -= part;
//                   }
//                    int count = inflater3.inflate(buffer);
//                    outputStream.write(buffer, 0, count);
//                } catch (DataFormatException ex) {
//                    System.out.println("Packet decompression error: "+ex.getMessage());
//                    return new CryptResult(raw, 1);
//                }
//            }
//            try {
//                outputStream.close();
//            } catch (IOException ex) {
//            }
//            return new CryptResult(outputStream.toByteArray(), 1);
//        }
//        return new CryptResult(raw, 1);
    }
}
