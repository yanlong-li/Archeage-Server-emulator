using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCommons.Encryption
{
    class CtoSEncrypt
    {
        /*        // вх.парам:
                // ecx=48f0d794
                // ebx=20
                // edi=20
                //char __usercall CtoSEncrypt_2FCBD5h_5571@<al>(int a1@<ecx>, int a2@<ebx>, int a3@<edi>)
                unsafe char CtoSEncrypt_2FCBD5h_5571(int a1, int a2, int a3)
                {
                    int** v3; // esi
                    char result; // al
                    int cry_; // [esp+4h] [ebp-4h]

                    v3 = (int**)(a1 + 88);
                    cry_ = encrypt_E96E(*(int**)(a1 + 88)) + 0x2FCBD5; // 3132373;
                    CtoSEncrypt_F973(v3, a2, a3, &cry_);
                    result = Convert.ToChar(cry_ & 0xF7);
                    if (!Convert.ToBoolean(cry_ & 0xF70000))
                        result = Convert.ToChar(-2); //0xFE
                    return result;
                }
                //char __usercall CtoSEncrypt_2FA245h_54B6@<al>(int a1@<ecx>, int a2@<ebx>, int a3@<edi>)
                unsafe char CtoSEncrypt_2FA245h_54B6(int a1, int a2, int a3)
                {
                    int** v3; // esi
                    char result; // al
                    int cry_; // [esp+4h] [ebp-4h]

                    v3 = (int**)(a1 + 96);
                    cry_ = encrypt_E96E(*(int**)(a1 + 96)) + 0x2FA245; // 3121733;
                    CtoSEncrypt_F973(v3, a2, a3, &cry_);
                    result = Convert.ToChar((cry_ >> 14) & 0x73);
                    if (!Convert.ToBoolean(result))
                        result = Convert.ToChar(-2); //0xFE
                    return result;
                }
                //это не точная замена
                int CryRandom()
                {
                    // генерируем 4 случайных байта
                    Random random = new Random();
                    int rnd = random.Next(255);
                    rnd += random.Next(255) << 8;
                    rnd += random.Next(255) << 16;
                    rnd += random.Next(255) << 24;
                    return rnd;
                }

                //int __thiscall _encrypt_E927(int *this, void *Src)
                unsafe int _encrypt_E927(int* _this_, void* Src)
                {
                    int* v2; // ST10_4
                    v2 = _this_;
                    *v2 += CryRandom();
                    return encrypt_case_09F0(*v2, v2 + 1, Src, 4u);
                }

                unsafe int encrypt_E96E(int* _this_)
                {
                    int Dst; // [esp+4h] [ebp-4h]

                    encrypt_case_08C0(*_this_, &Dst, _this_ + 1, 4u);
                    return Dst;
                }

                //int __thiscall _encrypt_EC8A(int*this, void* Src)
                unsafe int _encrypt_EC8A(int* _this_, void* Src)
                {
                    int* v2; // ST10_4

                    v2 = _this_;
                    *v2 += CryRandom();
                    encrypt_case_09F0(*v2, v2 + 1, Src, 4u);
                    return *(_DWORD*)Src;
                }
                int __thiscall _encrypt_ECC3(int*this, void* Src)
                {
                    int* v2; // ST10_4

                    v2 = this;
                    *v2 += CryRandom();
                    return encrypt_case_09F0(*v2, v2 + 1, Src, 4u);
                }
                int __thiscall _encrypt_ECF7(int*this)
                {
                    int Dst; // [esp+4h] [ebp-4h]

                    encrypt_case_08C0(*this, &Dst, this + 1, 4u);
                    return Dst;
                }
                int* __thiscall encrypt_F181(int*this, int* a2)
                {
                    int* v2; // ST10_4
                    int Dst; // [esp+4h] [ebp-8h]
                    int Src; // [esp+8h] [ebp-4h]

                    v2 = this;
                    encrypt_case_08C0(*a2, &Dst, a2 + 1, 4u);
                    Src = Dst;
                    *v2 += CryRandom();
                    encrypt_case_09F0(*v2, v2 + 1, &Src, 4u);
                    return v2;
                }
                unsafe int CtoSEncrypt_F973(int** _this_, int a2, int a3, void* Src)
                {
                    int** v4; // eax
                    int* v5; // ecx
                    int* v6; // eax
                    int* v7; // ST1C_4
                    int* v9; // [esp+0h] [ebp-34h]
                    int** v10; // [esp+8h] [ebp-2Ch]

                    v10 = _this_;
                    v4 = _this_;
                    v5 = (int*)((char*)_this_[1] - 1);
                    v4[1] = v5;
                    if (!v5)
                    {
                        v6 = (int*)checkLimits_5A7F(a2, a3, 20);
                        if (v6)
                            v9 = encrypt_F181(v6, *v10);
                        else
                            v9 = 0;
                        checkLimits_5A91(*v10);
                        *v10 = v9;
                        v10[1] = (int*)1000;
                    }
                    v7 = *v10;
                    *v7 += CryRandom();
                    return encrypt_case_09F0(*v7, v7 + 1, Src, 4u);
                }

                int __userpurge _encrypt_FE19@<eax>(int** a1@<ecx>, int a2@<ebx>, int a3@<edi>, void* Src)
        {
          CtoSEncrypt_F973(a1, a2, a3, Src);
          return *(_DWORD*) Src;
            }
            int __thiscall encrypt_1C8D(int*this, unsigned int a2)
            {
                int v2; // eax
                int v3; // edx
                unsigned int v4; // ecx

                v2 = *this;
                v3 = this[1];
                v4 = a2;
                if (v3 - v2 <= a2)
                    std::_Xout_of_range("invalid vector<T> subscript");
                return v4 + v2;
            }
            int __thiscall _encrypt_34B3(int**this)
            {
                return encrypt_E96E(*this);
            }
            int* __thiscall _encrypt_34D7(int*this, void* Src)
            {
                int* v2; // esi

                v2 = this;
                _encrypt_E927(this, Src);
                return v2;
            }
            int __thiscall _encrypt_4598(int**this)
            {
                return encrypt_E96E(*this);
            }
            unsafe int encrypt_case_08C0(int _this_, void* Dst, void* Src, size_t Size)
            {
                int result; // eax

                result = _this_ % 0xAu;
                switch (_this_ % 0xAu)
                {
                    case 0u:
                        result = (int)sub_memcpy_0500(Dst, Src, Size);
                        break;
                    case 1u:
                        result = sub_memcpy_0560(Dst, Src, Size);
                        break;
                    case 2u:
                        result = sub_memcpy_05C0(Dst, Src, Size);
                        break;
                    case 3u:
                        result = sub_memcpy_0620(Dst, Src, Size);
                        break;
                    case 4u:
                        result = sub_memcpy_0680(Dst, Src, Size);
                        break;
                    case 5u:
                        result = sub_memcpy_06E0(Dst, Src, Size);
                        break;
                    case 6u:
                        result = sub_memcpy_0740(Dst, Src, Size);
                        break;
                    case 7u:
                        result = sub_memcpy_07A0(Dst, Src, Size);
                        break;
                    case 8u:
                        result = sub_memcpy_0800(Dst, Src, Size);
                        break;
                    case 9u:
                        result = sub_memcpy_0860(Dst, Src, Size);
                        break;
                    default:
                        return result;
                }
                return result;
            }
            unsafe int encrypt_case_09F0(int a1, void* Dst, void* Src, size_t Size)
            {
                int result; // eax

                result = a1 % 0xAu;
                switch (a1 % 0xAu)
                {
                    case 0u:
                        result = (int)sub_memcpy_0530(Dst, Src, Size);
                        break;
                    case 1u:
                        result = (int)sub_memcpy_0590(Dst, Src, Size);
                        break;
                    case 2u:
                        result = sub_memcpy_05F0(Dst, Src, Size);
                        break;
                    case 3u:
                        result = sub_memcpy_0650(Dst, Src, Size);
                        break;
                    case 4u:
                        result = sub_memcpy_06B0(Dst, Src, Size);
                        break;
                    case 5u:
                        result = sub_memcpy_0710(Dst, Src, Size);
                        break;
                    case 6u:
                        result = sub_memcpy_0770(Dst, Src, Size);
                        break;
                    case 7u:
                        result = sub_memcpy_07D0(Dst, Src, Size);
                        break;
                    case 8u:
                        result = sub_memcpy_0830(Dst, Src, Size);
                        break;
                    case 9u:
                        result = sub_memcpy_0890(Dst, Src, Size);
                        break;
                    default:
                        return result;
                }
                return result;
            }
            int _encrypt_1D50()
            {
                int result; // eax
                LARGE_INTEGER Frequency; // [esp+0h] [ebp-8h]

                result = QueryPerformanceFrequency(&Frequency);
                if (!Frequency.LowPart)
                {
                    encrypt_case_08C0(0, 0, 0, 0);
                    result = encrypt_case_09F0(0, 0, 0, 0);
                }
                return result;
        */
    }
}
