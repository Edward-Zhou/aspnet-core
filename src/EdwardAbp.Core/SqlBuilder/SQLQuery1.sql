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




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE orderpro 
	@tenantId int
AS
BEGIN
	SET NOCOUNT ON;

    Select COUNT(Orders.Id) AS Total From Orders
	Left Join OrderItems on Orders.Id = OrderItems.OrderId
	Where Orders.TenantId = @tenantId
	For Json AUTO
	Select Orders.Id as [Id], OrderItems.Title as [Title] From Orders
	Left Join OrderItems on Orders.Id = OrderItems.OrderId
	Where Orders.TenantId = @tenantId
	For Json AUTO
END
GO

