select * from (
select 
Ps.PhaseTitle,Pv.ProvinceCode,Pv.ProvinceTitleNep,Ds.DistrcitCode,Ds.DistrictNameNep,Vdc.VdcMunCode
,Vdc.VdcMunNameNep,Ms.SectionName,spm.SubProgramId,spm.SubProgramTitle,spm.TimeDurationYear,
spm.TotalBudget
from SubProgramMaster spm
inner join OfficeDetails OD on spm.OfficeId=OD.OfficeId
inner join Province Pv on Pv.ProvinceId=OD.ProvincesId
left join DistrictSetup Ds on Ds.DistrcitCode=OD.DistrictCode
left join VdcMun Vdc on Vdc.VdcMunCode=OD.VDCMUNCode
inner join ProgramPhaseStatus Ps on Ps.PhaseNumber=spm.PhaseStatus
inner join MainSection Ms on Ms.MainSectionId=spm.MainSectionId
where OD.UserType=4 and spm.ApprovedStatus=1 and spm.GrantTypeId=2 
and spm.PhaseStatus=3 and spm.TimeDurationYear>2
union all
select 
Ps.PhaseTitle,Pv.ProvinceCode,Pv.ProvinceTitleNep,Ds.DistrcitCode,Ds.DistrictNameNep,Vdc.VdcMunCode
,Vdc.VdcMunNameNep,Ms.SectionName,spm.SubProgramId,spm.SubProgramTitle,spm.TimeDurationYear,
spm.TotalBudget
from SubProgramMaster spm
inner join OfficeDetails OD on spm.OfficeId=OD.OfficeId
inner join Province Pv on Pv.ProvinceId=OD.ProvincesId
left join DistrictSetup Ds on Ds.DistrcitCode=OD.DistrictCode
left join VdcMun Vdc on Vdc.VdcMunCode=OD.VDCMUNCode
inner join ProgramPhaseStatus Ps on Ps.PhaseNumber=spm.PhaseStatus
inner join MainSection Ms on Ms.MainSectionId=spm.MainSectionId
where OD.UserType=4 and spm.ApprovedStatus=1 and spm.GrantTypeId=2 
and spm.PhaseStatus=4 and spm.TimeDurationYear>=2

union all
select 
Ps.PhaseTitle,Pv.ProvinceCode,Pv.ProvinceTitleNep,Ds.DistrcitCode,Ds.DistrictNameNep,Vdc.VdcMunCode
,Vdc.VdcMunNameNep,Ms.SectionName,spm.SubProgramId,spm.SubProgramTitle,spm.TimeDurationYear,
spm.TotalBudget
from SubProgramMaster spm
inner join OfficeDetails OD on spm.OfficeId=OD.OfficeId
inner join Province Pv on Pv.ProvinceId=OD.ProvincesId
left join DistrictSetup Ds on Ds.DistrcitCode=OD.DistrictCode
left join VdcMun Vdc on Vdc.VdcMunCode=OD.VDCMUNCode
inner join ProgramPhaseStatus Ps on Ps.PhaseNumber=spm.PhaseStatus
inner join MainSection Ms on Ms.MainSectionId=spm.MainSectionId
where OD.UserType=4 and spm.ApprovedStatus=1 and spm.GrantTypeId=2 
and spm.PhaseStatus=5 and spm.TimeDurationYear>=1

) t
where t.DistrcitCode=103
order by t.ProvinceCode,t.DistrcitCode,t.VdcMunCode
--19075



