﻿using System;
using System.Data.Common;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;
using WebPersonal.Shared.Data.Db;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class SkillRepository : BaseRepository<SkillEntity>
    {
        public override string TableName => TableNames.Skill;

        public SkillRepository(TransactionalWrapper conexion) : base(conexion)
        {
        }
    }
}
