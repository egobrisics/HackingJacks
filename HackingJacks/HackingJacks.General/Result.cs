using System;

namespace HackingJacks.General
{
    [Serializable()]
    public class Result<t> : Result
    {
        private t m_tItem;

        public t Item
        {
            get
            {
                return m_tItem;
            }
            set
            {
                m_tItem = value;
            }
        }

        public Result() : base()
        {
            m_tItem = default(t);
        }

        public Result(t tItem) : base()
        {
            m_tItem = tItem;
        }

        public Result(Exception ex) : base(ex)
        {
            if (typeof(t) == typeof(bool))
                m_tItem = (t)(object)false;
        }

        public Result(Exception ex, t tItem) : base(ex)
        {
            m_tItem = tItem;
        }



        private void SetError()
        {
            if (m_excError != null)
                return;

            m_excError = new Exception(m_strErrorMessage);
        }

        private void SetErrorMessage()
        {
            if (m_excError == null)
            {
                m_strErrorMessage = string.Empty;
                return;
            }

            m_strErrorMessage = m_excError.Message;
        }
    }

    public class Result
    {
        protected bool m_blnSuccess;
        protected Exception m_excError;
        protected string m_strErrorMessage;


        public bool Success
        {
            get
            {
                return m_blnSuccess;
            }
            set
            {
                m_blnSuccess = value;
            }
        }

        public Exception Error
        {
            get
            {
                return m_excError;
            }
            set
            {
                m_excError = value;
                SetErrorMessage();
            }
        }

        public string ErrorMessage
        {
            get
            {
                return m_strErrorMessage;
            }
            set
            {
                m_strErrorMessage = value;
                SetError();
            }
        }



        public Result()
        {
            m_blnSuccess = true;
            m_excError = null;
            SetErrorMessage();
        }

        public Result(Exception ex)
        {
            m_blnSuccess = false;
            m_excError = ex;
            SetErrorMessage();
        }



        private void SetError()
        {
            if (m_excError != null)
                return;

            m_excError = new Exception(m_strErrorMessage);
        }

        private void SetErrorMessage()
        {
            if (m_excError == null)
            {
                m_strErrorMessage = string.Empty;
                return;
            }

            m_strErrorMessage = m_excError.Message;
        }
    }
}
