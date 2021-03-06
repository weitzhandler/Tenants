﻿using Microsoft.Data.Entity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework.Tenant
{ 
  public class TenantUserStore<TUser, TRole, TTenant, TContext> : TenantUserStore<TUser, TRole, TTenant, TContext, string, TenantUserLogin, IdentityUserRole<string>, IdentityUserClaim<string>>
    where TUser : TenantUser
    where TRole : IdentityRole<string>
    where TTenant : Tenant
    where TContext : TenantIdentityDbContext<TUser, TRole, TTenant, string, TenantUserLogin, IdentityUserRole<string>, IdentityUserClaim<string>>
  {
    public TenantUserStore(TContext context)
      : base(context)
    {
    }
  }

  public class TenantUserStore<
    TUser, TRole, TTenant, TContext, TKey,
    TUserLogin, TUserRole, TUserClaim>
    : UserStore<TUser, TRole, TContext, TKey>
    where TUser : TenantUser<TKey>
    where TRole : IdentityRole<TKey>
    where TTenant : Tenant<TKey>
    where TContext : TenantIdentityDbContext<
      TUser, TRole, TTenant, TKey, TUserLogin, TUserRole, TUserClaim>
    where TKey : IEquatable<TKey>
    where TUserLogin : TenantUserLogin<TKey>, new()
    where TUserRole : IdentityUserRole<TKey>, new()
    where TUserClaim : IdentityUserClaim<TKey>, new()
  {
    public TenantUserStore(TContext context)
      : base(context)
    {
    }

    private DbSet<TUserLogin> _Logins;
    public DbSet<TUserLogin> Logins
    {
      get
      {
        return _Logins ?? (_Logins = Context.Set<TUserLogin>());
      }
    }

    public override Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
    {
      return base.CreateAsync(user, cancellationToken);
    }

    public override Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken))
    {
      return base.FindByNameAsync(normalizedUserName, cancellationToken);
    }

    public override Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return base.FindByIdAsync(userId, cancellationToken);
    }

    public override Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken = default(CancellationToken))
    {
      return base.AddLoginAsync(user, login, cancellationToken);
    }

    public override Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
    {
      return base.FindByEmailAsync(normalizedEmail, cancellationToken);
    }
  }
}