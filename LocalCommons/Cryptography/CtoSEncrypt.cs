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

char __usercall CtoSEncrypt_80F1@<al>(int a1@<ecx>, int a2@<ebx>)
{
  int v2; // esi
  char result; // al
  int Src; // [esp+4h] [ebp-4h]

  v2 = a1 + 0x58;
  Src = XOR_E96E(*(a1 + 0x58)) + 0x2FCBD5;
  XOR_F973(v2, a2, &Src);
  result = BYTE2(Src) & 0xF7;
  if ( !(Src & 0xF70000) )
    result = 0xFEu;
  return result;
}

int __thiscall XOR_E96E(int *this)
{
  int Dst; // [esp+4h] [ebp-4h]

  XOR_case_08C0(*this, &Dst, this + 1, 4u);
  return Dst;
}

непосредсвенно работа с пакетом _XOR_case_08C0
параметры:
CPU Stack 1 проход
Address   Value      ASCII>Comments
0015FBFC  /FE12EE87  c?¦  ; |Arg1 = FE12EE87
0015FC00  |0015FC10  ?   ; |Arg2 = 15FC10
0015FC04  |2EA25C10  \o.  ; |Arg3 = 2EA25C10
0015FC08  |00000004       ; \Arg4 = 4
(ecx=0x2EA25C0C)

CPU Stack 2 проход
Address  Value      ASCII>Comments
EBP-18   /B7773810  8w¬  ; |Arg1 = B7773810
EBP-14   |0015FC10  ?   ; |Arg2 = 15FC10
EBP-10   |2EA25C10  \o.  ; |Arg3 = 2EA25C10
EBP-C    |00000004       ; \Arg4 = 4


int __cdecl XOR_case_08C0(int a1:Arg1, void *Dst:Arg2, void *Src:Arg3, size_t Size:Arg4)
{
  int result; // eax

  result = a1 % 0xAu;
  switch ( a1 % 0xAu )
  {
    case 0u:
      result = sub_A0C0500(Dst, Src, Size);
      break;
    case 1u:
      result = sub_A0C0560(Dst, Src, Size);
      break;
    case 2u:
      result = sub_A0C05C0(Dst, Src, Size);
      break;
    case 3u:
      result = sub_A0C0620(Dst, Src, Size);
      break;
    case 4u:
      result = sub_A0C0680(Dst, Src, Size);
      break;
    case 5u:
      result = sub_A0C06E0(Dst, Src, Size);
      break;
    case 6u:
      result = sub_A0C0740(Dst, Src, Size);
      break;
    case 7u:
      result = sub_A0C07A0(Dst, Src, Size);
      break;
    case 8u:
      result = sub_A0C0800(Dst, Src, Size);
      break;
    case 9u:
      result = sub_A0C0860(Dst, Src, Size);
      break;
    default:
      return result;
  }
  return result;
}

int __userpurge XOR_F973@<eax>(int a1@<ecx>, int a2@<ebx>, void *Src)
{
  int v3; // eax
  int v4; // ecx
  int *v5; // eax
  int *v6; // ST1C_4
  int *v8; // [esp+0h] [ebp-34h]
  int v9; // [esp+8h] [ebp-2Ch]

  v9 = a1;
  v3 = a1;
  v4 = *(a1 + 4) - 1;
  *(v3 + 4) = v4;
  if ( !v4 )
  {
    v5 = max_numeric_limits_5A7F(a2, 20);
    if ( v5 )
      v8 = XOR_F181(v5, *v9);
    else
      v8 = 0;
    max_numeric_limits_5FDB5A91(*v9);
    *v9 = v8;
    *(v9 + 4) = 1000;
  }
  v6 = *v9;
  *v6 += CryRandom();
  return XOR_case_09F0(*v6, v6 + 1, Src, 4u);
}
int __cdecl XOR_case_09F0(int a1, void *Dst, void *Src, size_t Size)
{
  int result; // eax

  result = a1 % 0xAu;
  switch ( a1 % 0xAu )
  {
    case 0u:
      result = sub_A0C0530(Dst, Src, Size);
      break;
    case 1u:
      result = sub_A0C0590(Dst, Src, Size);
      break;
    case 2u:
      result = sub_A0C05F0(Dst, Src, Size);
      break;
    case 3u:
      result = sub_A0C0650(Dst, Src, Size);
      break;
    case 4u:
      result = sub_A0C06B0(Dst, Src, Size);
      break;
    case 5u:
      result = sub_A0C0710(Dst, Src, Size);
      break;
    case 6u:
      result = sub_A0C0770(Dst, Src, Size);
      break;
    case 7u:
      result = sub_A0C07D0(Dst, Src, Size);
      break;
    case 8u:
      result = sub_A0C0830(Dst, Src, Size);
      break;
    case 9u:
      result = sub_A0C0890(Dst, Src, Size);
      break;
    default:
      return result;
  }
  return result;
}          */
    }
}
