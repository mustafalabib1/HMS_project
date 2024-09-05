
CREATE or alter PROCEDURE InsertActiveSubstance @ActiveSubstanceName NVARCHAR(100)
AS
BEGIN
    -- Check if the active substance already exists
    IF EXISTS (SELECT 1 FROM ActiveSubstances WHERE ActiveSubstancesName = @ActiveSubstanceName)
    BEGIN
        -- If it exists, raise an error
        RAISERROR('Active substance already exists.', 16, 1);
        RETURN;
    END

    -- Insert the new active substance
    INSERT INTO ActiveSubstances (ActiveSubstancesName)
    VALUES (@ActiveSubstanceName);
END;
go
-----------------------------------------------------------------------------------------------------------------------------
CREATE or alter PROC AddMedication 
@MedicationCode VARCHAR(20),
@MedicationName VARCHAR(255), 
@MedicationStrenght int ,
@ActiveSubstanceName VARCHAR(255),
@PharmacyID int =1

AS
BEGIN
	-- Check if the medication already exists
    IF EXISTS (SELECT 1 FROM Medication WHERE MedName = @MedicationName or MedicationCode= @MedicationCode)
    BEGIN
        -- If it exists, raise an error
        RAISERROR('medication already exists.', 16, 1);
        RETURN;
    END
    -- Declare variable to hold the ActiveSubstanceID
    DECLARE @ActiveSubstanceID INT;

    -- Check if the ActiveSubstance already exists
    SELECT @ActiveSubstanceID = [ActiveSubstancesID]
    FROM [dbo].[ActiveSubstances]
    WHERE  [ActiveSubstancesName]= @ActiveSubstanceName;

    -- If the ActiveSubstance does not exist, insert it
    IF @ActiveSubstanceID IS NULL
    BEGIN
        EXEC [dbo].[InsertActiveSubstance] @ActiveSubstanceName;
        -- Get the newly inserted ActiveSubstanceID
        SET @ActiveSubstanceID = SCOPE_IDENTITY();
    END

    -- Insert the new medication with the ActiveSubstanceID
    INSERT INTO Medication
    VALUES (@MedicationCode,@MedicationName,@MedicationStrenght,@PharmacyID);
	insert into [dbo].[Medications_ActiveSubstance]
	values (@ActiveSubstanceID,@MedicationCode)
END;

go
-------------------------------------------------------------------------------------------------------------------------
-- Test the stored procedure AddMedication

-- Example 1: Adding a new medication with a new active substance
EXEC dbo.AddMedication 
    @MedicationCode = 'MED001',
    @MedicationName = 'ExampleMed1',
    @MedicationStrenght = 500,
    @ActiveSubstanceName = 'NewActiveSubstance',
    @PharmacyID = 1;

-- Example 2: Adding a new medication with an existing active substance
EXEC  [dbo].[AddMedication]
    @MedicationCode = 'MED002',
    @MedicationName = 'ExampleMed2',
    @MedicationStrenght = 250,
    @ActiveSubstanceName = 'Paracetamol',  -- This should match an existing active substance
	@PharmacyID = 1;

-- Example 3: Adding another new medication with the same new active substance
EXEC dbo.AddMedication 
    @MedicationCode = 'MED003',
    @MedicationName = 'ExampleMed3',
    @MedicationStrenght = 100,
    @ActiveSubstanceName = 'NewActiveSubstance',  -- Reusing the same active substance as Example 1
    @PharmacyID = 1;

EXEC [dbo].[InsertActiveSubstance]'Paracetamol';
------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER FUNCTION GetPatientMedicationsAndActiveSubstancesByPatientID( @PatientID INT )
RETURNS TABLE
AS
RETURN
(
    select m.MedName,a.ActiveSubstancesName ,pm.DateIssued 
	from Patient p inner join Patient_Medications pm
	on p.PatientID=pm.PatientID inner join Medication m
	on m.MedicationCode= pm.MedicationCode inner join  Medications_ActiveSubstance am
	on m.MedicationCode= am.MedicationCode inner join ActiveSubstances a
	on a.ActiveSubstancesID=am.ActiveSubstancesID
    WHERE 
        P.PatientID = @PatientID
);
go
select * 
from GetPatientMedicationsAndActiveSubstancesByPatientID(1000)
 


