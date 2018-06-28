declare @organizationUnitId int = 30566
declare @deviceId int = 50729;

--All Coupons
Select * From Coupons
Where Id in 
(
	Select Id From (
		Select Id,OrganizationUnitId From Coupons
		Union
		Select CouponId, OrganizationUnitId From DispatchedCoupon
    ) as T
	Where OrganizationUnitId is Null
	--for ou Where OrganizationUnitId = @organizationUnitId
)
And TenantId = 3028
And IsDeleted = 0
And Title like '%Test%'
 

--Published Coupons
Select * From Coupons
Where Id in
(
	Select CouponId From DeviceCoupons
	Where DeviceId = @deviceId
)
And IsDeleted = 0

--UnPublished Coupons
Select * From Coupons
Where Id in
(
	Select Id From (
		Select Id,OrganizationUnitId From Coupons
		Union
		Select CouponId, OrganizationUnitId From DispatchedCoupon
    ) as T
	Where OrganizationUnitId is Null
	--for ou Where OrganizationUnitId = @organizationUnitId
	And Id not in
	(
		Select CouponId From DeviceCoupons
		Where DeviceId = @deviceId
	)
)
And TenantId = 3028
And IsDeleted = 0
