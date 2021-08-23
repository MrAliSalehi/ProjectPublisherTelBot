using NewBot.Models.Model;
using System;
using System.Linq;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using NewBot.Models.CustomModel;

namespace NewBot.Models.Controller
{
    public class DbController
    {
        #region Public Refrences
        public static string Conn => connstring;
        private static readonly string connstring = "Data Source=qwxp\\SQL2019;Initial Catalog=telbotZB_db;User ID=bot;Password=jokerr123";
        #endregion

        #region UserControllers

        #region Get
        [Obsolete]
        public user GetUser(user user)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    user = db.users.Where(p => p.uID == user.uID || p.PK == user.PK).Select(p => p).ToList().SingleOrDefault();
                    return user;
                }
            }
            catch (Exception)
            {
                //fm.ExeptionHandler($"DbController>>GetUser>>1nd TryCatch Throwed.[{exp.Message}]", 30);
                throw;
            }
        }
        #endregion

        #region Insert
        [Obsolete]
        public bool InsertNewUser(user usr)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    if (!db.users.Any(p => p.uID == usr.uID))
                    {
                        db.users.Add(usr);
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                //fm.ExeptionHandler($"DbController>>InsertNewUser>>1nd TryCatch Throwed.[{exp.Message}]", 45);
                throw;
            }
        }
        #endregion

        #region Update
        public bool UpdateUser(user user)
        {
            try
            {
                using (var db = new telbotZB_dbEntities())
                {
                    var result = db.users.SingleOrDefault(b => b.uID == user.uID);
                    if (result != null)
                    {
                        if (user.adsStep != null)
                        {
                            result.adsStep = user.adsStep;
                        }
                        if (user.fullname != null)
                        {
                            result.fullname = user.fullname;
                        }
                        if (user.number != null)
                        {
                            result.number = user.number;
                        }
                        if (user.agent != null)
                        {
                            result.agent = user.agent;
                        }
                        if (user.finishedregister != null)
                        {
                            result.finishedregister = user.finishedregister;
                        }
                        if (user.invcode != null)
                        {
                            result.invcode = user.invcode;
                        }
                        if (user.registerstep != null)
                        {
                            result.registerstep = user.registerstep;
                        }
                        if (user.registermode != null)
                        {
                            result.registermode = user.registermode;
                        }
                        if (user.projectstep != null)
                        {
                            result.projectstep = user.projectstep;
                        }
                        if (user.ishireing != null)
                        {
                            result.ishireing = user.ishireing;
                        }
                        if (user.EmployeeID != null)
                        {
                            result.EmployeeID = user.EmployeeID;
                        }
                        if (user.IsBanned != null)
                        {
                            result.IsBanned = user.IsBanned;
                        }
                        if (user.sendtoEMPmessageID != null)
                        {
                            result.sendtoEMPmessageID = user.sendtoEMPmessageID;
                        }
                        if (user.DisableStep != null)
                        {
                            result.DisableStep = user.DisableStep;
                        }
                        if (user.AdsChance != null)
                        {
                            result.AdsChance = user.AdsChance;
                        }
                        if (user.HireChance != null)
                        {
                            result.HireChance = user.HireChance;
                        }
                        if (user.ProjectChance != null)
                        {
                            result.ProjectChance = user.ProjectChance;
                        }
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #endregion

        #region AgentControllers

        #region Get
        public agent GetAgent(agent agn)
        {
            try
            {
                using (var db = new telbotZB_dbEntities())
                {
                    var outP = db.agents.Where(p => p.agentuid == agn.agentuid).FirstOrDefault();
                    if (outP == null)
                    {
                        return null;
                    }
                    else
                    {
                        return outP;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Insert
        [Obsolete]
        public bool AddSubMember(int AgentPrimaryKey, string CustomerId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connstring))
                {
                    string query = string.Format
                        ("if exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'agent{0}') begin if exists(select customeruID from agent{1} where customeruID={2}) begin return end else begin insert into agent{3} (customeruID) values ({4}) end end;",
                        AgentPrimaryKey, AgentPrimaryKey, CustomerId, AgentPrimaryKey, CustomerId);
                    db.Execute(query);
                    return true;
                }
            }
            catch (Exception)
            {
                //fm.ExeptionHandler($"DbController>>AddSubMember>> 1nd TryCatch Throwed.[{e.Message}]", 89);
                return false;
            }
        }
        public bool UpdateAgentMemberName(string uid, int agentid, string name)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connstring))
                {
                    string query = string.Format
                        ("if exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'agent{0}') begin if exists(select customeruID from agent{1} where customeruID={2}) begin insert into agent{3} (name) values (N'{4}') end else begin return end end;", agentid, agentid, uid, agentid, name);
                    db.Execute(query);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateAgentMemberNumber(string uid, int agentid, string number)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connstring))
                {
                    string query = string.Format
                        ("if exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'agent{0}') begin if exists(select customeruID from agent{1} where customeruID={2}) begin insert into agent{3} (number) values ('{4}') end else begin return end end;", agentid, agentid, uid, agentid, number);
                    db.Execute(query);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        public string AddNewAgent(agent agent)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    var v = db.agents.Any(p => p.agentuid != agent.agentuid.ToString());
                    if (!v)
                    {
                        var g = db.agents.Add(new agent() { agentname = agent.agentname, agentnumber = agent.agentnumber, agentuid = agent.agentuid, FreeBalance = 0, sellcount = 0, UsedBalance = 0 });
                        int j = db.SaveChanges();
                        return "OK";
                    }
                    return "no";
                }
            }
            catch (DbUpdateException z)
            {
                return $"mess:[{z.Message}]\n\ndata:[{z.Data}\n\nentries:[{z.Entries}]]";
                throw;
            }

        }
        #endregion

        #region Update
        public bool UpdateAgent(agent ag)
        {
            try
            {
                using (var db = new telbotZB_dbEntities())
                {
                    var res = db.agents.Where(p => p.agentuid == ag.agentuid).FirstOrDefault();
                    if (res != null)
                    {
                        if (ag.FreeBalance != null)
                        {
                            res.FreeBalance = ag.FreeBalance;
                        }
                        if (ag.sellcount != null)
                        {
                            res.sellcount = ag.sellcount;
                        }
                        if (ag.UsedBalance != null)
                        {
                            res.UsedBalance = ag.UsedBalance;
                        }
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion
        #endregion

        #region ProjectsController

        #region Get
        public bool TagExists(Project project)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    if (db.Projects.Any(p => p.ProjectId == project.ProjectId))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Project GetUserProject(Project project)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    project = db.Projects.Where(p => p.uid == project.uid && p.ProjectId == project.ProjectId).Select(p => p).ToList().SingleOrDefault();
                    return project;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Project> GetListOfProject(Project project)
        {
            try
            {

                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    List<Project> list = db.Projects.Where(p => p.uid == project.uid).Select(p => p).ToList();
                    return list;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Insert
        public string AddNewProject(Project project)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    if (!db.Projects.Any(p => p.uid == project.uid && p.ProjectFinished == false || p.ProjectFinished == null))
                    {
                        var t = db.Projects.Add(project);
                        db.SaveChanges();
                        return "OK";
                    }
                    else
                    {
                        return "exists";
                    }
                }
            }
            catch (Exception g)
            {
                return g.Message;
                throw;
            }
        }
        #endregion

        #region Update
        public bool UpdateUserProject(Project proj)
        {
            try
            {
                using (var db = new telbotZB_dbEntities())
                {
                    var result = db.Projects.SingleOrDefault(b => b.uid == proj.uid && b.ProjectFinished == false);
                    if (result != null)
                    {
                        if (proj.ProjectId != null)
                        {
                            result.ProjectId = proj.ProjectId;
                        }
                        if (proj.category != null)
                        {
                            result.category = proj.category;
                        }
                        if (proj.dicription != null)
                        {
                            result.dicription = proj.dicription;
                        }
                        if (proj.Checked != null)
                        {
                            result.Checked = proj.Checked;
                        }
                        if (proj.ProjectFinished != null)
                        {
                            result.ProjectFinished = proj.ProjectFinished;
                        }
                        if (proj.ChnnlMssgID != null)
                        {
                            result.ChnnlMssgID = proj.ChnnlMssgID;
                        }
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool ConfirmUserProject(Project proj)
        {
            try
            {
                using (var db = new telbotZB_dbEntities())
                {
                    var result = db.Projects.SingleOrDefault(b => b.uid == proj.uid && b.ProjectId == proj.ProjectId);
                    if (result != null)
                    {
                        result.Checked = proj.Checked;
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateUserProjectByTag(Project proj)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    var res = db.Projects.Where(p => p.uid == proj.uid && p.ProjectId == proj.ProjectId).SingleOrDefault();
                    if (res != null)
                    {
                        if (proj.disable != null)
                        {
                            res.disable = proj.disable;
                        }
                        if (proj.ChnnlMssgID != null)
                        {
                            res.ChnnlMssgID = proj.ChnnlMssgID;
                        }
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Remove

        public async Task<bool> CancelUserProjectAsync(Project proj)
        {
            try
            {
                using (var db = new telbotZB_dbEntities())
                {
                    var find = await db.Projects.FirstOrDefaultAsync(p => p.uid == proj.uid && p.ProjectId == proj.ProjectId);
                    if (find != null)
                    {
                        db.Projects.Remove(find);
                        await db.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveUserProject(Project proj)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    var findproject = db.Projects.Where(p => p.uid == proj.uid && p.ProjectFinished == false || p.ProjectFinished == null).FirstOrDefault();
                    if (findproject != null)
                    {
                        db.Projects.Remove(findproject);
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion
        #endregion

        #region RecruitmentController

        #region Get
        public List<HireList> GetAllUserHireProject(HireList hire, CallBackModel.ReturnMode mode)
        {
            try
            {
                using (var db = new telbotZB_dbEntities())
                {
                    if (mode == CallBackModel.ReturnMode.ALL)
                    {
                        List<HireList> res = db.HireLists.Where(p => p.employeeID == hire.employeeID && p.hirefinished == true).ToList();
                        return res != null ? res : null;
                    }
                    else if (mode == CallBackModel.ReturnMode.PassDisAndUnchecked)
                    {
                        List<HireList> res = db.HireLists.Where(p => p.employeeID == hire.employeeID && p.hirefinished == true && p.Disable != true && p.@checked == true).ToList();
                        return res != null ? res : null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public HireList GetHireProject(HireList hire)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    hire = db.HireLists.Where(p => p.employeeID == hire.employeeID && p.ProjectID == hire.ProjectID).Select(p => p).ToList().SingleOrDefault();
                    if (hire == null)
                    {
                        return null;
                    }
                    else
                    {
                        return hire;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        #endregion

        #region Insert
        public List<string> AddNewRec(HireList hire)
        {
            List<string> outp = new List<string>();
            try
            {

                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    if (!db.HireLists.Any(p => p.employeeID == hire.employeeID && p.hirefinished == false))
                    {
                        var t = db.HireLists.Add(hire);
                        db.SaveChanges();
                        return outp;
                    }
                    else
                    {
                        return outp;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    outp.Add($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outp.Add($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                return outp;
                throw;
            }
        }
        #endregion

        #region Update

        public bool UpdateHireByTag(HireList hire)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    var res = db.HireLists.Where(p => p.employeeID == hire.employeeID && p.ProjectID == hire.ProjectID).SingleOrDefault();
                    if (res != null)
                    {
                        if (hire.Disable != null)
                        {
                            res.Disable = hire.Disable;
                        }
                        if (hire.ChnnlMssgID != null)
                        {
                            res.ChnnlMssgID = hire.ChnnlMssgID;
                        }
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateHireProject(HireList hire)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    var result = db.HireLists.SingleOrDefault(b => b.employeeID == hire.employeeID && b.hirefinished == false);
                    if (result != null)
                    {
                        if (hire.@checked != null)
                        {
                            result.@checked = hire.@checked;
                        }
                        if (hire.discription != null)
                        {
                            result.discription = hire.discription;
                        }
                        if (hire.FreeLancerID != null)
                        {
                            result.FreeLancerID = hire.FreeLancerID;
                        }
                        if (hire.hirefinished != null)
                        {
                            result.hirefinished = hire.hirefinished;
                        }
                        if (hire.ProjectID != null)
                        {
                            result.ProjectID = hire.ProjectID;
                        }
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool ConfirmHireProject(HireList hire)
        {
            try
            {
                using (var db = new telbotZB_dbEntities())
                {
                    var result = db.HireLists.SingleOrDefault(b => b.employeeID == hire.employeeID && b.ProjectID == hire.ProjectID);
                    if (result != null)
                    {
                        result.@checked = hire.@checked;
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Remove

        public async Task<bool> CancelUserHireProjectAsync(HireList hire)
        {
            try
            {
                using (var db = new telbotZB_dbEntities())
                {
                    var find = await db.HireLists.FirstOrDefaultAsync(p => p.employeeID == hire.employeeID && p.ProjectID == hire.ProjectID);
                    if (find != null)
                    {
                        db.HireLists.Remove(find);
                        await db.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveHireProject(HireList hire)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    var FindPrj = db.HireLists.Where(p => p.employeeID == hire.employeeID && p.hirefinished == false || p.hirefinished == null).SingleOrDefault();
                    db.HireLists.Remove(FindPrj);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #endregion

        #region AdsController

        #region Ads CUID

        #region SelctionController
        public async Task<AdsOutPutModel> AdsAsync(AdsModel model)
        {
            var Out = new AdsOutPutModel();
            using (var db = new telbotZB_dbEntities())
            {
                switch (model.AdsType)
                {
                    #region Channel
                    case AdsType.Channel:
                        Out.AdsType = AdsType.Channel;
                        if (model.AdsChannel != null)
                        {
                            switch (model.AdsOperation)
                            {
                                #region Get
                                case AdsOperation.Get:
                                    var search = await db.AdsChannels.SingleOrDefaultAsync(p => p.uID == model.AdsChannel.uID && p.ProjectID == model.AdsChannel.ProjectID);
                                    Out.OutPutType = OutPutType.OBJECT;
                                    Out.OutPut = search;
                                    break;
                                #endregion

                                #region Insert
                                case AdsOperation.Insert:
                                    db.AdsChannels.Add(new AdsChannel() { uID = model.AdsChannel.uID, ProjectID = model.AdsChannel.ProjectID });
                                    await db.SaveChangesAsync();
                                    Out.OutPutType = OutPutType.BOOL;
                                    Out.OutPut = true;
                                    break;
                                #endregion

                                #region Update
                                case AdsOperation.Update:
                                    var find = await db.AdsChannels.SingleOrDefaultAsync(p =>
                                        p.uID == model.AdsChannel.uID && p.ProjectID == model.AdsChannel.ProjectID);
                                    Out.OutPutType = OutPutType.BOOL;
                                    if (find != null)
                                    {
                                        if (model.AdsChannel.Disciption != null)
                                        {
                                            find.Disciption = model.AdsChannel.Disciption;
                                        }
                                        if (model.AdsChannel.Link != null)
                                        {
                                            find.Link = model.AdsChannel.Link;
                                        }
                                        if (model.AdsChannel.IsChannel != null)
                                        {
                                            find.IsChannel = model.AdsChannel.IsChannel;
                                        }
                                        if (model.AdsChannel.Published != null)
                                        {
                                            find.Published = model.AdsChannel.Published;
                                        }
                                        await db.SaveChangesAsync();
                                        Out.OutPut = true;
                                    }
                                    else
                                    {
                                        Out.OutPut = false;
                                    }
                                    break;
                                #endregion

                                #region Delete
                                case AdsOperation.Delete:
                                    var IsExists = await db.AdsChannels.FirstOrDefaultAsync(p =>
                                         p.uID == model.AdsChannel.uID && p.ProjectID == model.AdsChannel.ProjectID);
                                    Out.OutPutType = OutPutType.BOOL;
                                    if (IsExists != null)
                                    {
                                        db.AdsChannels.Remove(IsExists);
                                        await db.SaveChangesAsync();
                                        Out.OutPut = true;
                                    }
                                    else
                                    {
                                        Out.OutPut = false;
                                    }
                                    break;
                                #endregion

                                #region Default
                                default:
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    #endregion

                    #region Group
                    case AdsType.Group:
                        Out.AdsType = AdsType.Group;
                        if (model.AdsGroup != null)
                        {
                            switch (model.AdsOperation)
                            {
                                #region Get
                                case AdsOperation.Get:
                                    var search = await db.AdsGroups.SingleOrDefaultAsync(p => p.uID == model.AdsGroup.uID && p.ProjectID == model.AdsGroup.ProjectID);
                                    Out.OutPutType = OutPutType.OBJECT;
                                    Out.OutPut = search;
                                    break;
                                #endregion

                                #region Insert
                                case AdsOperation.Insert:
                                    db.AdsGroups.Add(new AdsGroup() { uID = model.AdsGroup.uID, ProjectID = model.AdsGroup.ProjectID });
                                    await db.SaveChangesAsync();
                                    Out.OutPutType = OutPutType.BOOL;
                                    Out.OutPut = true;
                                    break;
                                #endregion

                                #region Update
                                case AdsOperation.Update:
                                    var find = await db.AdsGroups.SingleOrDefaultAsync(p =>
                                        p.uID == model.AdsGroup.uID && p.ProjectID == model.AdsGroup.ProjectID);
                                    Out.OutPutType = OutPutType.BOOL;
                                    if (find != null)
                                    {
                                        if (model.AdsGroup.Disciption != null)
                                        {
                                            find.Disciption = model.AdsGroup.Disciption;
                                        }
                                        if (model.AdsGroup.Link != null)
                                        {
                                            find.Link = model.AdsGroup.Link;
                                        }
                                        if (model.AdsGroup.IsGroup != null)
                                        {
                                            find.IsGroup = model.AdsGroup.IsGroup;
                                        }
                                        if (model.AdsGroup.Published != null)
                                        {
                                            find.Published = model.AdsGroup.Published;
                                        }
                                        await db.SaveChangesAsync();
                                        Out.OutPut = true;
                                    }
                                    else
                                    {
                                        Out.OutPut = false;
                                    }
                                    break;
                                #endregion

                                #region Delete
                                case AdsOperation.Delete:
                                    var IsExists = await db.AdsGroups.FirstOrDefaultAsync(p =>
                                         p.uID == model.AdsGroup.uID && p.ProjectID == model.AdsGroup.ProjectID);
                                    Out.OutPutType = OutPutType.BOOL;
                                    if (IsExists != null)
                                    {
                                        db.AdsGroups.Remove(IsExists);
                                        await db.SaveChangesAsync();
                                        Out.OutPut = true;
                                    }
                                    else
                                    {
                                        Out.OutPut = false;
                                    }
                                    break;
                                #endregion

                                #region Default
                                default:
                                    break;
                                    #endregion
                            }
                        }
                        break;

                    #endregion

                    #region Business
                    case AdsType.Business:
                        if (model.AdsBusiness != null)
                        {
                            Out.AdsType = AdsType.Business;
                            switch (model.AdsOperation)
                            {
                                #region Get
                                case AdsOperation.Get:
                                    var search = await db.AdsBusinesses.SingleOrDefaultAsync(p =>
                                        p.uID == model.AdsBusiness.uID && p.ProjectID == model.AdsBusiness.ProjectID);
                                    Out.OutPutType = OutPutType.OBJECT;
                                    Out.OutPut = search;
                                    break;
                                #endregion

                                #region Insert
                                case AdsOperation.Insert:
                                    db.AdsBusinesses.Add(new AdsBusiness() { uID = model.AdsBusiness.uID, ProjectID = model.AdsBusiness.ProjectID });
                                    await db.SaveChangesAsync();
                                    Out.OutPutType = OutPutType.BOOL;
                                    Out.OutPut = true;
                                    break;
                                #endregion

                                #region Update
                                case AdsOperation.Update:
                                    var find = await db.AdsBusinesses.SingleOrDefaultAsync(p =>
                                        p.uID == model.AdsBusiness.uID && p.ProjectID == model.AdsBusiness.ProjectID);
                                    Out.OutPutType = OutPutType.BOOL;
                                    if (find != null)
                                    {
                                        if (model.AdsBusiness.Published != null)
                                        {
                                            find.Published = model.AdsBusiness.Published;
                                        }
                                        if (model.AdsBusiness.PictureUID != null)
                                        {
                                            find.PictureUID = model.AdsBusiness.PictureUID;
                                        }
                                        if (model.AdsBusiness.Discription != null)
                                        {
                                            find.Discription = model.AdsBusiness.Discription;
                                        }
                                        await db.SaveChangesAsync();
                                        Out.OutPut = true;
                                    }
                                    else
                                    {
                                        Out.OutPut = false;
                                    }
                                    break;
                                #endregion

                                #region Delete
                                case AdsOperation.Delete:
                                    Out.OutPutType = OutPutType.BOOL;
                                    var isExists = await db.AdsBusinesses.FirstOrDefaultAsync(p =>
                                        p.uID == model.AdsBusiness.uID && p.ProjectID == model.AdsBusiness.ProjectID);
                                    if (isExists != null)
                                    {
                                        db.AdsBusinesses.Remove(isExists);
                                        await db.SaveChangesAsync();
                                        Out.OutPut = false;
                                    }
                                    else
                                    {
                                        Out.OutPut = false;
                                    }
                                    break;
                                #endregion

                                #region Default
                                default:
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
            }

            return Out;
        }
        #endregion

        #region Get
        public List<ADSList> GetUserAdsList(ADSList ads)
        {
            try
            {
                using (var db = new telbotZB_dbEntities())
                {
                    var res = db.ADSLists.Where(p => p.uID == ads.uID).ToList();
                    if (res == null)
                    {
                        return null;
                    }
                    else
                    {
                        return res;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ADSList GetAds(ADSList ads)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    ads = db.ADSLists.Where(p => p.uID == ads.uID && p.GUID == ads.GUID).Select(p => p).ToList().SingleOrDefault();
                    return ads;
                }
            }
            catch (Exception)
            {
                //fm.ExeptionHandler($"DbController>>GetUser>>1nd TryCatch Throwed.[{exp.Message}]", 30);
                throw;
            }
        }
        public List<ADSList> GetUnPublishedAds()
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    var publist = db.ADSLists.Where(p => p.IsPublished == false || p.IsPublished == null).ToList();
                    return publist;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<ADSList> GetAdsByIDList(List<int> idies)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    List<ADSList> res = new List<ADSList>();
                    foreach (var id in idies)
                    {
                        res.Add(db.ADSLists.Where(p => p.ID == id).FirstOrDefault());
                    }
                    return res;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Insert
        public bool InsertNewAds(ADSList ads)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    if (!db.ADSLists.Any(p => p.uID == ads.uID && p.link == null && p.discription == null && p.GUID == null))
                    {
                        db.ADSLists.Add(new ADSList() { uID = ads.uID, OneLiner = ads.OneLiner });
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                //fm.ExeptionHandler($"DbController>>InsertNewUser>>1nd TryCatch Throwed.[{exp.Message}]", 45);
                throw;
            }
        }
        #endregion

        #region Update
        public bool UpdateAds(ADSList ads)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    var result = db.ADSLists.SingleOrDefault(b => b.uID == ads.uID && ads.discription != null ? b.discription == null : true && ads.link != null ? b.link == null : true && ads.pic != null ? b.pic == null : true);
                    if (result != null)
                    {
                        if (ads.pic != null)
                        {
                            result.pic = ads.pic;
                        }
                        if (ads.link != null)
                        {
                            result.link = ads.link;
                        }
                        if (ads.discription != null)
                        {
                            result.discription = ads.discription;
                        }
                        if (ads.GUID != null)
                        {
                            result.GUID = ads.GUID;
                        }
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool UpdateAdsByIDList(List<int> idies)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {

                    foreach (var id in idies)
                    {
                        var res = db.ADSLists.SingleOrDefault(p => p.ID == id && p.OneLiner == true);
                        res.IsPublished = true;
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #endregion

        #region AdsImageController

        #region Get
        public async Task<Image> GetImageAsync(Image img)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    return await db.Images.SingleOrDefaultAsync(p => p.uID == img.uID && p.ProjectID == img.ProjectID && p.UniqueID == img.UniqueID);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Insert
        public async Task<bool> InsertNewImageAsync(Image image)
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    if (!db.Images.Any(I => I.ProjectID == image.ProjectID))
                    {
                        db.Images.Add(new Image()
                        {
                            uID = image.uID,
                            UniqueID = image.UniqueID,
                            FileID = image.FileID,
                            ProjectID = image.ProjectID,
                            Discription = image.Discription
                        });
                        await db.SaveChangesAsync();
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                //fm.ExeptionHandler($"DbController>>InsertNewUser>>1nd TryCatch Throwed.[{exp.Message}]", 45);
                throw;
            }
        }
        #endregion

        #endregion

        #endregion

        #region AdminsController
        public List<admin> GetAllAdmins()
        {
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    List<admin> admins = new List<admin>();
                    admins = db.admins.ToList();
                    return admins;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region DataBase Management
        public bool CreateTable(int agentid)
        {
            string query = string.Format($"if exists(select * from INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'agent{agentid}')begin return end else begin CREATE TABLE agent{agentid}(customeruID int,name nvarchar(250),number text)end;");
            try
            {
                using (telbotZB_dbEntities db = new telbotZB_dbEntities())
                {
                    var h = db.Database.ExecuteSqlCommand(query);
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Search Tag Into All 3 Tables Of Project&Ads&Hire
        /// </summary>
        /// <param name="Tag">Enter Tag,# Is Not Required</param>
        /// <returns>Model OF Tag That Can Be Project,Hire Or ADS</returns>
        public TagViewModel SearchTag(string Tag)
        {
            try
            {
                using (var db = new telbotZB_dbEntities())
                {
                    #region Search Tag Inside Tables
                    #region Get Tables
                    var FindInProjects = db.Projects.Where(p => p.ProjectId == Tag).SingleOrDefault();
                    var FindInHireList = db.HireLists.Where(p => p.ProjectID == Tag).SingleOrDefault();
                    var FindInADSList = db.ADSLists.Where(p => p.GUID == Tag).SingleOrDefault();
                    #endregion

                    #region Search
                    List<object> SearchResult = new List<object>() { FindInProjects, FindInHireList, FindInADSList };
                    int FindIndex = 404;
                    int i = 0;
                    foreach (var res in SearchResult)
                    {
                        if (res != null)
                        {
                            FindIndex = i;
                            break;
                        }
                        i++;
                    }
                    #endregion
                    #endregion

                    #region Return OutPut
                    TagViewModel OutPut = new TagViewModel() { };
                    switch (i)
                    {
                        #region Tag Not Found
                        case 404:
                            OutPut.TagIdentifier = TagType.Null;
                            break;
                        #endregion

                        #region Tag Is Project
                        case 0:
                            OutPut = new TagViewModel()
                            {
                                Category = FindInProjects.category,
                                Discription = FindInProjects.dicription,
                                Tag = FindInProjects.ProjectId,
                                ID = FindInProjects.ID,
                                TagIdentifier = TagType.Project
                            };
                            break;
                        #endregion

                        #region Tag Is Hire
                        case 1:
                            OutPut = new TagViewModel()
                            {
                                Discription = FindInHireList.discription,
                                Tag = FindInHireList.ProjectID,
                                ID = FindInHireList.ID,
                                TagIdentifier = TagType.Hire
                            };
                            break;
                        #endregion

                        #region Tag Is Ads
                        case 2:
                            OutPut = new TagViewModel()
                            {
                                Discription = FindInADSList.discription,
                                Tag = FindInADSList.GUID,
                                ID = FindInADSList.ID,
                                Link = FindInADSList.link,
                                Pic = FindInADSList.pic == null ? null : FindInADSList.pic,
                                TagIdentifier = TagType.Ads
                            };
                            break;
                        #endregion
                        default:
                            OutPut.TagIdentifier = TagType.Null;
                            break;
                    }
                    return OutPut;
                    #endregion
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
