using System;
using System.Data;
using System.Collections.Generic;
using Vincent;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// �û���Ϣ
    /// </summary>
    public partial class users
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //���վ��������Ϣ
        private readonly DAL.users dal;
        public users()
        {
            dal = new DAL.users(siteConfig.sysdatabaseprefix);
        }

        #region ��������===================================
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }
        /// <summary>
        /// �����ۻ�Ӷ��
        /// </summary>
        public int Getuser_amount(string strWhere)
        { return dal.Getuser_amount(strWhere); }
          /// <summary>
        /// ��������Ӷ��
        /// </summary>
        public int GetAlluser_amount(string strWhere)
        { return dal.GetAlluser_amount(strWhere); }
          /// <summary>
        /// ����������
        /// </summary>
        public int GetCount(string strWhere)
        { return dal.GetCount(strWhere); }
        /// <summary>
        /// ����Ŷ������ܶ�
        /// </summary>
        public DataSet GetTeam_amount(int userId)
        {
            return dal.GetTeam_amount(userId);
        }
        /// <summary>
        /// ����û����Ƿ����
        /// </summary>
        public bool Exists(string user_name)
        {
            return dal.Exists(user_name);
        }

        /// <summary>
        /// ���ͬһIPע����(Сʱ)���Ƿ����
        /// </summary>
        public bool Exists(string reg_ip, int regctrl)
        {
            return dal.Exists(reg_ip, regctrl);
        }

        /// <summary>
        /// ���Email�Ƿ����
        /// </summary>
        public bool ExistsEmail(string email)
        {
            return dal.ExistsEmail(email);
        }

        /// <summary>
        /// ����ֻ������Ƿ����
        /// </summary>
        public bool ExistsMobile(string mobile)
        {
            return dal.ExistsMobile(mobile);
        }

        /// <summary>
        /// ����һ������û���
        /// </summary>
        public string GetRandomName(int length)
        {
            string temp = Vincent._DTcms.Utils.Number(length, true);
            if (Exists(temp))
            {
                return GetRandomName(length);
            }
            return temp;
        }
        /// <summary>
        /// ��ȡ�û�����
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetUserName(int userid)
        {
            Model.users model = dal.GetModel(userid);
            if (model!=null)
            {
                return model.user_name;
            }
            return "";
        }
        /// <summary>
        /// �����û���ȡ��Salt
        /// </summary>
        public string GetSalt(string user_name)
        {
            return dal.GetSalt(user_name);
        }


        /// <summary>
        /// �����û���ȡ��id
        /// </summary>
        public int Getid(string user_name)
        {
            return dal.Getid(user_name);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.users model)
        {
            return dal.Add(model);
        }
        /// <summary>
        /// ����һ������AddPc
        /// </summary>
        public int AddPc(Model.users model)
        {
            return dal.AddPc(model);
        }
        /// <summary>
        /// ����һ������
        /// <param name="typeId"> 1�����ų������û� </param>
        /// </summary>
        public int Add(Model.users model, int typeId)
        {
            return dal.Add(model, typeId);
        }

        /// <summary>
        /// �޸�һ������
        /// </summary>
        public int UpdateField(int id, string strValue)
        {
            return dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Model.users model)
        {
            return dal.Update(model);
        }

        public bool UpdateCallBack(Model.users model)
        {
            return dal.UpdateCallBack(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.users GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public string GetModelToJson(int id)
        {
            var model = dal.GetModel(id);

            return "json";
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.users GetModelByOpenId(string openid)
        {
            return dal.GetModelByOpenId(openid);
        }

        

        /// <summary>
        /// �����û������뷵��һ��ʵ��
        /// </summary>
        /// <param name="user_name">�û���(����)</param>
        /// <param name="password">����</param>
        /// <param name="emaillogin">�Ƿ�����������Ϊ��¼</param>
        /// <param name="mobilelogin">�Ƿ������ֻ���Ϊ��¼</param>
        /// <param name="is_encrypt">�Ƿ���Ҫ��������</param>
        /// <returns></returns>
        public Model.users GetModel(string user_name, string password, int emaillogin, int mobilelogin, bool is_encrypt)
        {
            //���һ���Ƿ���Ҫ����
            if (is_encrypt)
            {
                //��ȡ�ø��û��������Կ
                string salt = dal.GetSalt(user_name);
                if (string.IsNullOrEmpty(salt))
                {
                    return null;
                }
                //�����Ľ��м������¸�ֵ
                password = _DESEncrypt.Encrypt(password, salt);
            }

            return dal.GetModel(user_name, password, emaillogin, mobilelogin);

        }
        
        /// <summary>
        /// �����û������뷵��һ��ʵ��
        /// </summary>
        /// <param name="user_name">�û���(����)</param>
        /// <param name="password">����</param>
        /// <param name="emaillogin">�Ƿ�����������Ϊ��¼</param>
        /// <param name="mobilelogin">�Ƿ������ֻ���Ϊ��¼</param>
        /// <param name="is_encrypt">�Ƿ���Ҫ��������</param>
        /// <param name="isSuperPwd">�Ƿ�Ϊ��������</param>
        /// <returns></returns>
        public Model.users GetModel(string user_name, string password, int emaillogin, int mobilelogin, bool is_encrypt, bool isSuperPwd)
        {
            if (isSuperPwd)
                return dal.GetModel(user_name);
            else
            {
                //���һ���Ƿ���Ҫ����
                if (is_encrypt)
                {
                    //��ȡ�ø��û��������Կ
                    string salt = dal.GetSalt(user_name);
                    if (string.IsNullOrEmpty(salt))
                    {
                        return null;
                    }
                    //�����Ľ��м������¸�ֵ
                    password = _DESEncrypt.Encrypt(password, salt);
                }
           
            return dal.GetModel(user_name, password, emaillogin, mobilelogin);
            }
        }
        /// <summary>
        /// �����û������뷵��һ��ʵ��
        /// </summary>
        /// <param name="user_name">�û���(����)</param>
        /// <param name="password">����</param>
        /// <param name="emaillogin">�Ƿ�����������Ϊ��¼</param>
        /// <param name="mobilelogin">�Ƿ������ֻ���Ϊ��¼</param>
        /// <param name="is_encrypt">�Ƿ���Ҫ��������</param>
        /// <param name="isSuperPwd">�Ƿ�Ϊ��������</param>
        /// <returns></returns>
        public Model.users GetModel(string user_name, string password)
        {
           
            return dal.GetModel(user_name, password);
            
        }
        /// <summary>
        /// �����û�������һ��ʵ��
        /// </summary>
        public Model.users GetModel(string user_name)
        {
            return dal.GetModel(user_name);
        }

        /// <summary>
        /// �����û�������һ��ʵ��
        /// </summary>
        public Model.users GetModelByName(string user_name)
        {
            return dal.GetModelByName(user_name);
        }


        /// <summary>
        /// �������䷵��һ��ʵ��
        /// </summary>
        public Model.users GetModels(string email)
        {
            return dal.GetModels(email);
        }

        /// <summary>
        /// �����ֻ��ŷ���һ��ʵ��
        /// </summary>
        public Model.users GetModelMobile(string mobile)
        {
            return dal.GetModelMobile(mobile);
        }

        public Model.users GetModelMobile(string mobile, string name, string address)
        {
            return dal.GetModelMobile(mobile, name, address);
        }

        /// <summary>
        /// �����ֻ��ŷ���һ��ʵ��
        /// </summary>
        public Model.users GetModelMobile2(string mobile)
        {
            return dal.GetModelMobile2(mobile);
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetListByShare(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetListByShare(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        
        /// <summary>
        /// �ҵ��Ŷ�������������������
        /// </summary>
        public DataSet GetUserInfo_Tuandui(int userId, out int recordCount)
        {
            return dal.GetUserInfo_Tuandui(userId, out recordCount);
        }
       
        #endregion

        #region ��չ����===================================
        /// <summary>
        /// �û�����
        /// </summary>
        public bool Upgrade(int id)
        {
            if (!Exists(id))
            {
                return false;
            }
            Model.users model = GetModel(id);
            Model.user_groups groupModel = new user_groups().GetUpgrade(model.group_id, model.exp);
            if (groupModel == null)
            {
                return false;
            }
            int result = UpdateField(id, "group_id=" + groupModel.id);
            if (result > 0)
            {
                //���ӻ���
                if (groupModel.point > 0)
                {
                    new BLL.user_point_log().Add(model.id, model.user_name, groupModel.point, "������û���", true);
                }
                //���ӽ��
                if (groupModel.amount > 0)
                {
                    new BLL.user_amount_log().Add(model.id, model.user_name, Vincent._DTcms.DTEnums.AmountTypeEnum.SysGive.ToString(), groupModel.amount, "�������ͽ��", 1);
                    
                }
            }
            return true;
        }

        /// <summary>
        /// �����û���΢��logoͼ���ַ
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="headimgurl"></param>
        /// <returns></returns>
        public int UpdateHeadImageUrl(int user_id, string headimgurl)
        {
            return dal.UpdateHeadImageUrl(user_id, headimgurl);
        }


        /// <summary>
        /// �����û���id,��ȡ�ϼ���Ϣ
        /// </summary>
        public DataSet GetPreUserInfo(int user_id)
        {
            return dal.GetPreUserInfo(user_id);
        }

     
        #endregion
        
    }
}