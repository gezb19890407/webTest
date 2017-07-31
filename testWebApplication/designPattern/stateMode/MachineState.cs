using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWebApplication.designPattern.stateMode
{

    public interface OpenDoorState
    {
        void open();

        void close();
    }

    public class GeneralOpenDoorState
    {
        private List<string> list;
        public List<string> stringList
        {
            get
            {
                if (list == null)
                {
                    list = new List<string>();
                }
                return list;
            }
        }


        private OpenDoorState state;


        public GeneralOpenDoorState(OpenDoorState state)
        {
            this.state = state;
        }

        public OpenDoorState State
        {
            get { return state; }
            set { state = value; }
        }


        public void open()
        {

        }

        public void close()
        {

        }
    }

    public class OpenDoorEvent : OpenDoorState
    {
        GeneralOpenDoorState generalOpenDoorState;

        public OpenDoorEvent(GeneralOpenDoorState generalOpenDoorState)
        {
            this.generalOpenDoorState = generalOpenDoorState;
        }

        public void close()
        {
            throw new NotImplementedException();
        }

        public void open()
        {
            generalOpenDoorState.stringList.Add("门的状态是打开");
            generalOpenDoorState.State = new CloseDoorEvent(generalOpenDoorState);
        }
    }

    public class CloseDoorEvent : OpenDoorState
    {
        GeneralOpenDoorState generalOpenDoorState;

        public CloseDoorEvent(GeneralOpenDoorState generalOpenDoorState)
        {
            this.generalOpenDoorState = generalOpenDoorState;
        }

        public void close()
        {
            generalOpenDoorState.stringList.Add("门的状态是关闭");
            generalOpenDoorState.State = new OpenDoorEvent(generalOpenDoorState);
        }

        public void open()
        {
            throw new NotImplementedException();
        }
    }
}