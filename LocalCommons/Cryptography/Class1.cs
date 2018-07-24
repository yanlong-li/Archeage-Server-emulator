using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCommons.Cryptography
{
    class CtoSEncrypt
    {
        /*

        // вх.парам:
        // ecx=48f0d794
        // ebx=20
        // edi=20
        char __usercall CtoSEncrypt_2FCBD5h_5571@<al>(int a1@<ecx>, int a2@<ebx>, int a3@<edi>)
        {
            int **v3; // esi
            char result; // al
            int cry_; // [esp+4h] [ebp-4h]

            v3 = (a1 + 0x58);
            cry_ = encrypt_E96E(*(a1 + 0x58)) + 0x2FCBD5;
            CtoSEncrypt_F973(v3, a2, a3, &cry_);
            result = BYTE2(cry_) & 0xF7;
            if ( !(cry_ & 0xF70000) )
              result = 0xFEu;
            return result;
        }
        char __usercall CtoSEncrypt_2FA245h_54B6@<al>(int a1@<ecx>, int a2@<ebx>, int a3@<edi>)
        {
            int **v3; // esi
            char result; // al
            unsigned int cry_; // [esp+4h] [ebp-4h]

            v3 = (a1 + 0x60);
            cry_ = encrypt_E96E(*(a1 + 0x60)) + 0x2FA245;
            CtoSEncrypt_F973(v3, a2, a3, &cry_);
            result = (cry_ >> 0xE) & 0x73;
            if ( !result )
              result = 0xFEu;
            return result;
        }  


          */
    }
}
