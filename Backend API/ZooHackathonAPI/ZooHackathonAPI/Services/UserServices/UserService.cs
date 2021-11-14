using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;
using ZooHackathonAPI.Models.User;
using ZooHackathonAPI.Repository.UserRepo;
using ZooHackathonAPI.Requests;
using ZooHackathonAPI.Responses;
using ZooHackathonAPI.Services.BaseServices;
using ZooHackathonAPI.Services.ReportServices;
using ZooHackathonAPI.UnitOfWorks;
using ZooHackathonAPI.Utilities;
using ZooHackathonAPI.ViewModels;

namespace ZooHackathonAPI.Services.UserServices
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly AutoMapper.IConfigurationProvider _mapperProvider;
        private readonly IMapper _mapper;

        private readonly IReportService _reportService;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper,
            IUserRepository repository, 
            IReportService reportService) : base(unitOfWork, repository)
        {
            _configuration = configuration;
            _mapperProvider = mapper.ConfigurationProvider;
            _mapper = mapper;

            _reportService = reportService;
        }

        public async Task<UserResponse> Login(string email, string password)
        {
            var user = await Get()
                .Where(tempUser => tempUser.Email.Equals(email) && tempUser.Password.Equals(password))
                .ProjectTo<UserDTO>(_mapperProvider)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var userReports = _reportService.GetReportsByUserId(user.Id);

                string token = TokenUtil.GenerateJWTToken(user, _configuration);

                var response = new UserResponse
                {
                    ID = user.Id, 
                    Token = token,
                    Email = user.Email,
                    FullName = user.FullName,
                    IsHideInfo = user.IsHideInfo,
                    ReportCount = userReports != null ? userReports.Count : 0
                };

                return await Task.Run(() => response);
            }

            throw new ErrorResponse((int)HttpStatusCode.Forbidden, "Wrong email or password.");
        }

        public async Task<UserResponse> Register(string email, string password, int role, bool isHideInfo)
        {
            var existUser = await Get()
                .Where(tempUser => tempUser.Email.Equals(email))
                .FirstOrDefaultAsync();

            if (existUser != null)
                throw new ErrorResponse((int)HttpStatusCode.Forbidden, "This email existed.");

            string fullName = email.Split('@')[0];

            var user = new UserDTO
            {
                Email = email,
                Password = password,
                FullName = fullName,
                Role = role,
                IsHideInfo = isHideInfo,
                CreateDate = DateTime.Now
            };

            var targetUser = _mapperProvider.CreateMapper().Map<User>(user);

            await CreateAsync(targetUser);

            var token = TokenUtil.GenerateJWTToken(user, _configuration);

            var response = new UserResponse
            {
                ID = targetUser.ID, 
                Token = token,
                Email = targetUser.Email,
                FullName = targetUser.Fullname, 
                IsHideInfo = targetUser.IsHideInfo, 
                ReportCount = 0
            };

            return await Task.Run(() => response);
        }

        public async Task<User> UpdateUser(UpdateUserRequest request)
        {
            var user = await GetAsync(request.Id);

            _mapper.Map(request, user);

            await UpdateAsync(user);

            return await Task.Run(() => user);
        }

        public async Task<UserResponse> GetUserByToken(string token)
        {
            TokenViewModel tokenModel = TokenUtil.ReadJWTTokenToModel(token, _configuration);

            int userId = tokenModel.Id;

            var user = await GetAsync(userId);

            var userReports = _reportService.GetReportsByUserId(userId);

            UserResponse response = new UserResponse
            {
                ID = userId, 
                Email = user.Email, 
                FullName = user.Fullname, 
                IsHideInfo = user.IsHideInfo, 
                Token = token, 
                ReportCount = userReports != null ? userReports.Count : 0
            };

            return await Task.Run(() => response);
        }

        public async Task<List<UserResponse>> GetAllUser()
        {
            var users = await Get().ToListAsync();

            List<UserResponse> userResponses = new();

            foreach (var temp in users)
            {
                var userReports = _reportService.GetReportsByUserId(temp.ID);

                UserResponse response = new UserResponse
                {
                    ID = temp.ID, 
                    Email = temp.Email,
                    FullName = temp.Fullname,
                    IsHideInfo = temp.IsHideInfo, 
                    ReportCount = userReports != null ? userReports.Count : 0
                };

                userResponses.Add(response);
            }

            var result = SortUserReportCount(userResponses);

            return await Task.Run(() => result);
        }

        private List<UserResponse> SortUserReportCount(List<UserResponse> userResponses)
        {
            var result = userResponses.AsQueryable().OrderByDescending(temp => temp.ReportCount).ToList();

            return result;
        }
    }
}
