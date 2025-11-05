namespace DedsiNative.DedsiUsers;

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
        Name = name;
        Email = email;
        MobilePhone = mobilePhone;
    }
    
    public string Id { get; private set; }
    
    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; private set; }
    
    /// <summary>
    /// 手机号：15888888888
    /// </summary>
    public string MobilePhone { get; private set; }

    /// <summary>
    /// 更新用户信息
    /// </summary>
    /// <param name="name">姓名</param>
    /// <param name="email">邮箱</param>
    /// <param name="mobilePhone">手机号</param>
    public void Update(string name, string email, string mobilePhone)
    {
        Name = name;
        Email = email;
        MobilePhone = mobilePhone;
    }
}