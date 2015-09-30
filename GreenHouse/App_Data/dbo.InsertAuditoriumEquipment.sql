CREATE PROCEDURE [dbo].[Procedure]
	@audId int,
	@addEqId int
AS
	INSERT INTO AuditoriumEquipment(Auditorium, AdditionalEquipment)
VALUES (@audId, @addEqId);
RETURN 0
