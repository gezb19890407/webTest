  


using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using testWebApplication.t4.Template;

namespace Project.Model
{
	/// <summary>
    /// String cantonId,String cantonCode,String cantonName,String pCantonCode,Int32 cantonLevel,String spacePointID,String rem,Int32 ix,Int32 disState,String createrId,DateTime createTime,String updaterId,DateTime updateTime,String deleteId,DateTime deleteTime
    /// </summary>
    [Serializable]
    public class baseAdministrativeDivision : DtoBaseEntity
    {
        #region Constructor
        public baseAdministrativeDivision() { }

        public baseAdministrativeDivision(String cantonId,String cantonCode,String cantonName,String pCantonCode,Int32 cantonLevel,String spacePointID,String rem,Int32 ix,Int32 disState,String createrId,DateTime createTime,String updaterId,DateTime updateTime,String deleteId,DateTime deleteTime)
        {
            this.cantonId = cantonId;
            this.cantonCode = cantonCode;
            this.cantonName = cantonName;
            this.pCantonCode = pCantonCode;
            this.cantonLevel = cantonLevel;
            this.spacePointID = spacePointID;
            this.rem = rem;
            this.ix = ix;
            this.disState = disState;
            this.createrId = createrId;
            this.createTime = createTime;
            this.updaterId = updaterId;
            this.updateTime = updateTime;
            this.deleteId = deleteId;
            this.deleteTime = deleteTime;
        }
        #endregion

        #region Attributes
        private String cantonId;

		/// <summary>
		/// CantonId
		/// <summary>
        public String CantonId
        {
            get { return cantonId; }
            set { cantonId = value; }
        }
        private String cantonCode;

		/// <summary>
		/// CantonCode
		/// <summary>
        public String CantonCode
        {
            get { return cantonCode; }
            set { cantonCode = value; }
        }
        private String cantonName;

		/// <summary>
		/// CantonName
		/// <summary>
        public String CantonName
        {
            get { return cantonName; }
            set { cantonName = value; }
        }
        private String pCantonCode;

		/// <summary>
		/// PCantonCode
		/// <summary>
        public String PCantonCode
        {
            get { return pCantonCode; }
            set { pCantonCode = value; }
        }
        private Int32 cantonLevel;

		/// <summary>
		/// CantonLevel
		/// <summary>
        public Int32 CantonLevel
        {
            get { return cantonLevel; }
            set { cantonLevel = value; }
        }
        private String spacePointID;

		/// <summary>
		/// SpacePointID
		/// <summary>
        public String SpacePointID
        {
            get { return spacePointID; }
            set { spacePointID = value; }
        }
        private String rem;

		/// <summary>
		/// Rem
		/// <summary>
        public String Rem
        {
            get { return rem; }
            set { rem = value; }
        }
        private Int32 ix;

		/// <summary>
		/// Ix
		/// <summary>
        public Int32 Ix
        {
            get { return ix; }
            set { ix = value; }
        }
        private Int32 disState;

		/// <summary>
		/// DisState
		/// <summary>
        public Int32 DisState
        {
            get { return disState; }
            set { disState = value; }
        }
        private String createrId;

		/// <summary>
		/// CreaterId
		/// <summary>
        public String CreaterId
        {
            get { return createrId; }
            set { createrId = value; }
        }
        private DateTime? createTime;

		/// <summary>
		/// CreateTime
		/// <summary>
        public DateTime? CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }
        private String updaterId;

		/// <summary>
		/// UpdaterId
		/// <summary>
        public String UpdaterId
        {
            get { return updaterId; }
            set { updaterId = value; }
        }
        private DateTime? updateTime;

		/// <summary>
		/// UpdateTime
		/// <summary>
        public DateTime? UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }
        private String deleteId;

		/// <summary>
		/// DeleteId
		/// <summary>
        public String DeleteId
        {
            get { return deleteId; }
            set { deleteId = value; }
        }
        private DateTime? deleteTime;

		/// <summary>
		/// DeleteTime
		/// <summary>
        public DateTime? DeleteTime
        {
            get { return deleteTime; }
            set { deleteTime = value; }
        }
        #endregion

        #region check
        
        public override List<string> check()
        {    
			List<string> errorList = new List<string>();
            bool validatorResult = true;
            if (string.IsNullOrEmpty(this.CantonId))
            {
                validatorResult = false;
                errorList.Add("CantonId不能为空!");
            }
            if (this.CantonId != null && 100 < this.CantonId.Length)
            {
                validatorResult = false;
                errorList.Add("CantonId的长度不能大于100！");
            }
            if (string.IsNullOrEmpty(this.CantonCode))
            {
                validatorResult = false;
                errorList.Add("CantonCode不能为空!");
            }
            if (this.CantonCode != null && 36 < this.CantonCode.Length)
            {
                validatorResult = false;
                errorList.Add("CantonCode的长度不能大于36！");
            }
            if (this.CantonName != null && 400 < this.CantonName.Length)
            {
                validatorResult = false;
                errorList.Add("CantonName的长度不能大于400！");
            }
            if (this.PCantonCode != null && 36 < this.PCantonCode.Length)
            {
                validatorResult = false;
                errorList.Add("PCantonCode的长度不能大于36！");
            }
            if (this.SpacePointID != null && 36 < this.SpacePointID.Length)
            {
                validatorResult = false;
                errorList.Add("SpacePointID的长度不能大于36！");
            }
            if (this.Rem != null && 256 < this.Rem.Length)
            {
                validatorResult = false;
                errorList.Add("Rem的长度不能大于256！");
            }
            if (string.IsNullOrEmpty(this.CreaterId))
            {
                validatorResult = false;
                errorList.Add("CreaterId不能为空!");
            }
            if (this.CreaterId != null && 36 < this.CreaterId.Length)
            {
                validatorResult = false;
                errorList.Add("CreaterId的长度不能大于36！");
            }
            if (this.CreateTime==null)
            {
                validatorResult = false;
                errorList.Add("CreateTime不能为空！");
            }
            if (this.UpdaterId != null && 36 < this.UpdaterId.Length)
            {
                validatorResult = false;
                errorList.Add("UpdaterId的长度不能大于36！");
            }
            if (this.DeleteId != null && 36 < this.DeleteId.Length)
            {
                validatorResult = false;
                errorList.Add("DeleteId的长度不能大于36！");
            }
            return errorList;
        }    
        #endregion
    }
}
