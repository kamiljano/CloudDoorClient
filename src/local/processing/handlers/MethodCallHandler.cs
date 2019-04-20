using System.Threading.Tasks;

namespace CloudDoorCs.Local {

    public interface MethodCallHandler<RES, REQ> {
        
        RES handle(REQ input);

    }

}