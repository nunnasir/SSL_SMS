﻿using AutoMapper;
using SSL_SMS.DAL;
using SSL_SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSL_SMS.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Data Model to Dto Model
            Mapper.CreateMap<Group, GroupDto>();
            Mapper.CreateMap<Contact, ContactDto>();

            //Dto Model to Data Model
            Mapper.CreateMap<GroupDto, Group>();
            Mapper.CreateMap<ContactDto, Contact>();
        }
    }
}