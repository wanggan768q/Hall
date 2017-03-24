using DateDeclare;
using System;
using UnityEngine;

public class InputChecking : MonoBehaviour
{
    protected bool _isCharacter(char c)
    {
        bool flag = false;
        if (((c < 'A') || (c > 'Z')) && ((c < 'a') || (c > 'z')))
        {
            return flag;
        }
        return true;
    }

    protected bool _isDigits(char c)
    {
        bool flag = false;
        if ((c >= '0') && (c <= '9'))
        {
            flag = true;
        }
        return flag;
    }

    protected bool _isUnderline(char c)
    {
        bool flag = false;
        if (c == '_')
        {
            flag = true;
        }
        return flag;
    }

    protected int CalculateCharCount(string str)
    {
        int num = 0;
        int num2 = 0;
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (char.IsDigit(str, i))
            {
                num++;
            }
            else if (char.IsWhiteSpace(str, i))
            {
                num3++;
            }
            else if ((char.ConvertToUtf32(str, i) >= Convert.ToInt32("4e00", 0x10)) && (char.ConvertToUtf32(str, i) <= Convert.ToInt32("9fff", 0x10)))
            {
                num4++;
            }
            else if (char.IsLetter(str, i))
            {
                num2++;
            }
            else
            {
                num5++;
            }
        }
        return ((((num + num2) + num3) + (num4 * 2)) + num5);
    }

    protected int CalculateWhiteSpace(string str)
    {
        int num = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (char.IsWhiteSpace(str, i))
            {
                num++;
            }
        }
        return num;
    }

    public EGameTipType CheckCoin(string value)
    {
        EGameTipType noneTip = EGameTipType.NoneTip;
        if ((value.CompareTo(string.Empty) == 0) || (value.CompareTo("-") == 0))
        {
            return EGameTipType.Awarding_Empty;
        }
        if (int.Parse(value) == 0)
        {
            return EGameTipType.Awarding_Empty;
        }
        if (int.Parse(value) < 0)
        {
            noneTip = EGameTipType.Awarding_Error;
        }
        return noneTip;
    }

    public EGameTipType CheckIDCardNumber(string strIDCardnumber)
    {
        EGameTipType noneTip = EGameTipType.NoneTip;
        if (((strIDCardnumber.CompareTo(string.Empty) == 0) || (strIDCardnumber.CompareTo("5-20个字符") == 0)) || (strIDCardnumber.CompareTo("5-20characters") == 0))
        {
            return EGameTipType.Register_IDNumberEmpty;
        }
        if ((this.CalculateCharCount(strIDCardnumber) >= 5) && (this.CalculateCharCount(strIDCardnumber) <= 20))
        {
            return noneTip;
        }
        return EGameTipType.Register_IDNmFormatError;
    }

    public EGameTipType CheckMoney(string value)
    {
        EGameTipType noneTip = EGameTipType.NoneTip;
        if ((value.CompareTo(string.Empty) == 0) || (value.CompareTo("-") == 0))
        {
            return EGameTipType.ReCharge_Error;
        }
        if (int.Parse(value) == 0)
        {
            return EGameTipType.ReCharge_Error;
        }
        if (int.Parse(value) < 0)
        {
            noneTip = EGameTipType.ReCharge_Error;
        }
        return noneTip;
    }

    public EGameTipType CheckNickname(string strNickname)
    {
        EGameTipType noneTip = EGameTipType.NoneTip;
        if (((strNickname.CompareTo(string.Empty) == 0) || (strNickname.CompareTo("10个字符以内，可使用汉字") == 0)) || (strNickname.CompareTo("10letters,can use Chinese letters") == 0))
        {
            return EGameTipType.Register_NicknameEmpty;
        }
        if (this.CalculateCharCount(strNickname) > 10)
        {
            return EGameTipType.Register_NicknameLengthError;
        }
        if (this.CalculateWhiteSpace(strNickname) == strNickname.Length)
        {
            noneTip = EGameTipType.Register_NicknameAllWhiteSpace;
        }
        return noneTip;
    }

    public EGameTipType CheckPassword(string strPasseord)
    {
        EGameTipType noneTip = EGameTipType.NoneTip;
        if (strPasseord.CompareTo(string.Empty) == 0)
        {
            return EGameTipType.UserPwdEmpty;
        }
        if ((this.CalculateCharCount(strPasseord) < 6) || (this.CalculateCharCount(strPasseord) > 0x10))
        {
            return EGameTipType.PwdLengthError;
        }
        for (int i = 0; i < strPasseord.Length; i++)
        {
            if (!this._isDigits(strPasseord[i]) && !this._isCharacter(strPasseord[i]))
            {
                noneTip = EGameTipType.PwdInputError;
                break;
            }
        }
        if (noneTip != EGameTipType.PwdInputError)
        {
            int num2 = 0;
            while (num2 < strPasseord.Length)
            {
                if (!this._isDigits(strPasseord[num2]))
                {
                    break;
                }
                num2++;
            }
            if (num2 == strPasseord.Length)
            {
                noneTip = EGameTipType.PwdInputError_Digit;
            }
            if (noneTip == EGameTipType.PwdInputError_Digit)
            {
                return noneTip;
            }
            num2 = 0;
            while (num2 < strPasseord.Length)
            {
                if (!this._isCharacter(strPasseord[num2]))
                {
                    break;
                }
                num2++;
            }
            if (num2 == strPasseord.Length)
            {
                noneTip = EGameTipType.PwdInputError_Charcter;
            }
        }
        return noneTip;
    }

    public EGameTipType CheckRecommender(string recommender)
    {
        EGameTipType noneTip = EGameTipType.NoneTip;
        if (recommender.CompareTo(string.Empty) == 0)
        {
            return EGameTipType.Recommender_Empty;
        }
        if ((this.CalculateCharCount(recommender) < 5) || (this.CalculateCharCount(recommender) > 15))
        {
            return EGameTipType.Recommender_Error;
        }
        for (int i = 0; i < recommender.Length; i++)
        {
            if (!this._isDigits(recommender[i]) && !this._isCharacter(recommender[i]))
            {
                return EGameTipType.Recommender_Error;
            }
        }
        return noneTip;
    }

    public EGameTipType CheckReMarkInfo(string str)
    {
        EGameTipType noneTip = EGameTipType.NoneTip;
        if (this.CalculateCharCount(str) > 400)
        {
            noneTip = EGameTipType.ReMarkInfo_Error;
        }
        return noneTip;
    }

    public EGameTipType CheckSafetyAnswer(string strAnswer)
    {
        EGameTipType noneTip = EGameTipType.NoneTip;
        if (((strAnswer.CompareTo(string.Empty) != 0) && (strAnswer.CompareTo("请输入正确答案") != 0)) && (strAnswer.CompareTo("Please enter the correct answer") != 0))
        {
            return noneTip;
        }
        return EGameTipType.Register_SafetyAnswerEmpty;
    }

    public EGameTipType CheckSafetyQuestion(string strQuestion)
    {
        EGameTipType noneTip = EGameTipType.NoneTip;
        if (((strQuestion.CompareTo(string.Empty) != 0) && (strQuestion.CompareTo("请选择安全问题") != 0)) && (strQuestion.CompareTo("Please select one") != 0))
        {
            return noneTip;
        }
        return EGameTipType.Register_SafetyProblemEmpty;
    }

    public EGameTipType CheckTeleNumber(string strTele)
    {
        EGameTipType noneTip = EGameTipType.NoneTip;
        if (((strTele.CompareTo(string.Empty) == 0) || (strTele.CompareTo("5-20个字符") == 0)) || (strTele.CompareTo("5-20characters") == 0))
        {
            return EGameTipType.Register_TeleNumberEmpty;
        }
        if ((this.CalculateCharCount(strTele) >= 5) && (this.CalculateCharCount(strTele) <= 20))
        {
            return noneTip;
        }
        return EGameTipType.Register_TeleNmFormatError;
    }

    public EGameTipType CheckUserID(string strUserID)
    {
        EGameTipType noneTip = EGameTipType.NoneTip;
        if (strUserID.CompareTo(string.Empty) == 0)
        {
            return EGameTipType.UserIDEmpty;
        }
        if (strUserID.CompareTo("5-15位字符、数字或下划线。") == 0)
        {
            return EGameTipType.UserIDEmpty;
        }
        if ((this.CalculateCharCount(strUserID) < 5) || (this.CalculateCharCount(strUserID) > 15))
        {
            return EGameTipType.Register_IDLengthError;
        }
        for (int i = 0; i < strUserID.Length; i++)
        {
            if ((!this._isDigits(strUserID[i]) && !this._isCharacter(strUserID[i])) && !this._isUnderline(strUserID[i]))
            {
                return EGameTipType.Register_IDInputError;
            }
        }
        return noneTip;
    }

    public EGameTipType Nickname(string strNickname)
    {
        EGameTipType noneTip = EGameTipType.NoneTip;
        if (((strNickname.CompareTo(string.Empty) == 0) || (strNickname.CompareTo("10个字符以内，可使用汉字") == 0)) || (strNickname.CompareTo("10letters,can use Chinese letters") == 0))
        {
            return EGameTipType.Register_NicknameEmpty;
        }
        if (this.CalculateCharCount(strNickname) > 10)
        {
            return EGameTipType.Register_NicknameLengthError;
        }
        if (this.CalculateWhiteSpace(strNickname) == strNickname.Length)
        {
            noneTip = EGameTipType.Register_NicknameAllWhiteSpace;
        }
        return noneTip;
    }
}

