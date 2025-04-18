using NetAdmin.Domain.Dto.Sys.SiteMsgDept;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     站内信-部门映射模块
/// </summary>
public interface ISiteMsgDeptModule : ICrudModule<CreateSiteMsgDeptReq, QuerySiteMsgDeptRsp // 创建类型
  , EditSiteMsgDeptReq                                                                      // 编辑类型
  , QuerySiteMsgDeptReq, QuerySiteMsgDeptRsp                                                // 查询类型
  , DelReq                                                                                  // 删除类型
>;