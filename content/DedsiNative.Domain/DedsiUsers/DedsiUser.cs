namespace DedsiNative.DedsiUsers;

using System;
using System.Text.RegularExpressions;

public class DedsiUser
{
    protected DedsiUser()
    {
        
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="mobilePhone"></param>
    public DedsiUser(string id, string name, string email, string mobilePhone)
    {
        Id = id;
        ChangeName(name);
        ChangeEmail(email);
        ChangeMobilePhone(mobilePhone);
    }
    
    // 基础校验所需的正则表达式
    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static readonly Regex CnMobileRegex = new(@"^1[3-9]\d{9}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string Id { get; private set; } = null!;
    
    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; private set; } = null!;
    
    public DedsiUser ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("姓名不能为空。", nameof(name));
        }
        
        Name = name.Trim();
        return this;
    }
    
    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; private set; } = null!;
    
    public DedsiUser ChangeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("邮箱不能为空。", nameof(email));
        }

        email = email.Trim();
        if (!EmailRegex.IsMatch(email))
        {
            throw new ArgumentException("邮箱格式不正确。", nameof(email));
        }
        
        Email = email;
        return this;
    }
    
    /// <summary>
    /// 手机号：15888888888
    /// </summary>
    public string MobilePhone { get; private set; } = null!;
    
    public DedsiUser ChangeMobilePhone(string mobilePhone)
    {
        if (string.IsNullOrWhiteSpace(mobilePhone))
        {
            throw new ArgumentException("手机号不能为空。", nameof(mobilePhone));
        }
        
        mobilePhone = mobilePhone.Trim();
        if (!CnMobileRegex.IsMatch(mobilePhone))
        {
            throw new ArgumentException("手机号格式不正确，应为中国大陆 11 位手机号，例如：15888888888。", nameof(mobilePhone));
        }
        
        MobilePhone = mobilePhone;
        return this;
    }

}