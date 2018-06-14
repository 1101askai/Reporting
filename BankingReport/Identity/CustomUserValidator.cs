using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Class CustomUserValidator is used for user validation.
/// </summary>
/// <typeparam name="TUser">The type of the t user.</typeparam>
public class CustomUserValidator<TUser> : IIdentityValidator<TUser> where TUser : class, Microsoft.AspNet.Identity.IUser
{
    /// <summary>
    /// The user manager
    /// </summary>
    private readonly UserManager<TUser> _userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomUserValidator{TUser}"/> class.
    /// </summary>
    /// <param name="manager">The manager.</param>
    public CustomUserValidator(UserManager<TUser> manager)
    {
        _userManager = manager;
    }
    /// <summary>
    /// validate as an asynchronous operation.
    /// </summary>
    /// <param name="user">The <see cref="TUser"/> user.</param>
    /// <returns><see cref="IdentityResult"/>.</returns>
    public async Task<IdentityResult> ValidateAsync(TUser user)
    {
        var errors = new List<string>();

        if (_userManager != null)
        {
            //check username availability. and add a custom error message to the returned errors list.
            var existingAccount = await _userManager.FindByNameAsync(user.UserName);
            if (existingAccount != null && existingAccount.Id != user.Id)
                errors.Add("Email " + user.UserName + " is already exist.");
        }

        //set the returned result (pass/fail) which can be read via the Identity Result returned from the CreateUserAsync
        return errors.Any()
            ? IdentityResult.Failed(errors.ToArray())
            : IdentityResult.Success;
    }
}