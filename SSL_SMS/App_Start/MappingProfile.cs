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
            Mapper.CreateMap<ContactGroup, ContactGroupDto>();
            Mapper.CreateMap<MessageGroup, MessageGroupDto>();

            //Dto Model to Data Model
            Mapper.CreateMap<ContactGroupDto, ContactGroup>();
            Mapper.CreateMap<MessageGroupDto, MessageGroup>();
        }
    }
}