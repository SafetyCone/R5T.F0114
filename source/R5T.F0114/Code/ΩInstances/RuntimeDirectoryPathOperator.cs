using System;


namespace R5T.F0114
{
    public class RuntimeDirectoryPathOperator : IRuntimeDirectoryPathOperator
    {
        #region Infrastructure

        public static IRuntimeDirectoryPathOperator Instance { get; } = new RuntimeDirectoryPathOperator();


        private RuntimeDirectoryPathOperator()
        {
        }

        #endregion
    }
}
