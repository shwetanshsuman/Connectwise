using ConnectwiseWebApplication.Models;

namespace ConnectwiseWebApplication.Gateways
{
    public interface IBranchFunction
    {
        public Task<IEnumerable<Branch>> GetBranches();
        public Task<IEnumerable<Branch>> GetBranchesByComapnyId(int branchId);
        public Task<Branch> GetBranchesById(int branchId);
        public Task<bool> DeleteBranch(int branchId);


        public bool EditBranch(Branch branch);
    }
}
