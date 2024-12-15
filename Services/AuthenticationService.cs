using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GYMFeeManagement_System_BE.Services

{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAunthenticationRepository _AuthenticationRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IAunthenticationRepository authenticationRepository, IConfiguration configuration)
        {
            _AuthenticationRepository = authenticationRepository;
            _configuration = configuration;
        }


        public async Task<string> Login(LoginRequestDTO request)
        {

            var staffDetails = await _AuthenticationRepository.GetUserByEmail(request.Email);

            if (staffDetails != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, staffDetails.PasswordHash))
                {
                    throw new Exception("Invalid password");
                }

                return GenerateToken(staffDetails);
            }


            var memberDetails = await _AuthenticationRepository.GetMemberByEmail(request.Email);

            if (memberDetails != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, memberDetails.PasswordHash))
                {
                    throw new Exception("Invalid password");
                }

                return GenerateTokenForMember(memberDetails);
            }

            throw new Exception("User not found");
        }


        public string GenerateTokenForMember(Member member)
        {
            var claimList = new List<Claim>
    {
        new Claim("UserId", member.MemberId.ToString()),
        new Claim("FullName", $"{member.FirstName} {member.LastName}"),
        new Claim("Email", member.Email),
        new Claim("UserRole", "Member"),
        new Claim("BranchId", member.BranchId.ToString())


    };

            var key = _configuration["Jwt:Key"];
            var secKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claimList,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateToken(Staff staff)
        {
            var claimList = new List<Claim>();
            claimList.Add(new Claim("UserId", staff.StaffId.ToString()));
            claimList.Add(new Claim("FullName", $"{staff.FirstName} {staff.LastName}"));
            claimList.Add(new Claim("Email", staff.Email));
            claimList.Add(new Claim("UserRole", staff.UserRole.ToString()));
            claimList.Add(new Claim("BranchId", staff.BranchId.ToString()));



            var key = _configuration["Jwt:Key"];
            var secKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var credintial = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claimList,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credintial
            );

            var res = new JwtSecurityTokenHandler().WriteToken(token);
            return res;
        }
    }
}
