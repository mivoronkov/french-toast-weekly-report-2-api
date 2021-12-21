using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class UserDtoTests
    {
        [Fact]
        public void ShouldCreateUserDto()
        {
            var followerL = new List<FollowerDto> { new FollowerDto() { ID = 2, FirstName = "Zorg", LastName = "Borg" } };
            var followerTM = new List<FollowerDto> { new FollowerDto() { ID = 3, FirstName = "Tzen", LastName = "Rock" } };

            var userDto = new UserDto
            {
                ID = 1,
                CompanyId = 1,
                Email = "1",
                FirstName = "A",
                LastName = "b",
                Title = "Last",
                Leaders = followerL,
                Teammates = followerTM,
                InviteLink = "asd"
            };
            Assert.Equal(1, userDto.ID);
            Assert.Equal(1, userDto.CompanyId);
            Assert.Equal(1, userDto.CompanyId);
            Assert.Equal("1", userDto.Email);
            Assert.Equal("A", userDto.FirstName);
            Assert.Equal("b", userDto.LastName);
            Assert.Equal("Last", userDto.Title);
            Assert.Equal(followerL, userDto.Leaders);
            Assert.Equal(followerTM, userDto.Teammates);
            Assert.Equal("asd", userDto.InviteLink);
        }
    }
}