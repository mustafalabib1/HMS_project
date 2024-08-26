-- Create the HMS database
CREATE DATABASE HMS;
GO

-- Use the newly created database
USE HMS;
GO

-- Create the Patient table
CREATE TABLE Patient
(
  PatientID INT NOT NULL identity(1,1),
  PatAddress NVARCHAR(100),
  SSN BIGINT NOT NULL,  
  PRIMARY KEY (PatientID)
  
);

-- Create the MedicalRecord table
CREATE TABLE MedicalRecord
(
  RecordID INT NOT NULL identity(1,1),
  Diagnosis NVARCHAR(250),
  CreatedDate DATE,
  LabResults NVARCHAR(250),
  PatientID INT NOT NULL,
  PRIMARY KEY (RecordID),
  FOREIGN KEY (PatientID) REFERENCES Patient(PatientID)
);

-- Create the ActiveSubstances table
CREATE TABLE ActiveSubstances
(
  ActiveSubstancesID INT NOT NULL identity(1,1),
  ActiveSubstancesName NVARCHAR(50) NOT NULL,
  PRIMARY KEY (ActiveSubstancesID)
);

-- Create the HMS_User table
CREATE TABLE HMS_User
(
  FirstName NVARCHAR(50) NOT NULL ,
  LastName NVARCHAR(50) NOT NULL,
  DateOfBirth DATE NOT NULL,
  Phone NVARCHAR(20) NOT NULL,
  Email NVARCHAR(50) NOT NULL,
  SSN BIGINT NOT NULL,
  User_Password NVARCHAR(50) NOT NULL,
  User_type CHAR(1) NOT NULL,
  gender CHAR(1),
  PRIMARY KEY (SSN)
);

-- Create the Pharmacy table
CREATE TABLE Pharmacy
(
  PharmacyId INT NOT NULL identity(1,1),
  Name NVARCHAR(50) NOT NULL,
  Phone NVARCHAR(20) NOT NULL,
  PRIMARY KEY (PharmacyId)
);

-- Create the Clinic table
CREATE TABLE Clinic
(
  ClinicID INT NOT NULL identity(1,1),
  ClinicName NVARCHAR(50) NOT NULL,
  Phone NVARCHAR(20) NOT NULL,
  Specialization NVARCHAR(50) NOT NULL,
  price MONEY NOT NULL,
  PRIMARY KEY (ClinicID)
);

-- Create the Nurses table
CREATE TABLE Nurses
(
  NurseID INT NOT NULL identity(1,1),
  ClinicID INT NOT NULL,
  SSN BIGINT NOT NULL,
  PRIMARY KEY (NurseID),
  FOREIGN KEY (ClinicID) REFERENCES Clinic(ClinicID),
  FOREIGN KEY (SSN) REFERENCES HMS_User(SSN)
);

-- Create the AvailableAppointment table
CREATE TABLE AvailableAppointment
(
  AvaAppDate DATE NOT NULL,
  ShiftNumber TINYINT NOT NULL,
  PRIMARY KEY (AvaAppDate, ShiftNumber)
);

-- Create the Pharmacist table
CREATE TABLE Pharmacist
(
  PharmacistID INT NOT NULL identity(1,1),
  PharmacyId INT NOT NULL,
  SSN BIGINT NOT NULL,
  PRIMARY KEY (PharmacistID),
  FOREIGN KEY (PharmacyId) REFERENCES Pharmacy(PharmacyId),
  FOREIGN KEY (SSN) REFERENCES HMS_User(SSN)
);

-- Create the Reception table
CREATE TABLE Reception
(
  ReceptionID INT NOT NULL identity(1,1),
  phone NVARCHAR(20) NOT NULL,
  PRIMARY KEY (ReceptionID)
);

-- Create the Receptionist table
CREATE TABLE Receptionist
(
  receptionistID INT NOT NULL identity(1,1),
  receptionID INT NOT NULL,
  SSN BIGINT NOT NULL,
  FOREIGN KEY (receptionID) REFERENCES Reception(ReceptionID),
  FOREIGN KEY (SSN) REFERENCES HMS_User(SSN)
);

-- Create the Patient_ActiveSubstances_Allergies table
CREATE TABLE Patient_ActiveSubstances_Allergies
(
  PatientID INT NOT NULL,
  ActiveSubstancesID INT NOT NULL,
  PRIMARY KEY (PatientID, ActiveSubstancesID),
  FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
  FOREIGN KEY (ActiveSubstancesID) REFERENCES ActiveSubstances(ActiveSubstancesID)
);

-- Create the Clinics_AvailableAppointments table
CREATE TABLE Clinics_AvailableAppointments
(
  Availableslots INT NOT NULL,
  ClinicID INT NOT NULL,
  AvaAppDate DATE NOT NULL,
  ShiftNumber TINYINT NOT NULL,
  PRIMARY KEY (ClinicID, AvaAppDate, ShiftNumber),
  FOREIGN KEY (ClinicID) REFERENCES Clinic(ClinicID),
  FOREIGN KEY (AvaAppDate, ShiftNumber) REFERENCES AvailableAppointment(AvaAppDate, ShiftNumber)
);

-- Create the ActiveSubstance_Interaction table
CREATE TABLE ActiveSubstance_Interaction
(
  ActiveSubstanceID1 INT NOT NULL,
  ActiveSubstanceID2 INT NOT NULL,
  Interaction nvarchar(100),
  PRIMARY KEY (ActiveSubstanceID1, ActiveSubstanceID2),
  FOREIGN KEY (ActiveSubstanceID1) REFERENCES ActiveSubstances(ActiveSubstancesID),
  FOREIGN KEY (ActiveSubstanceID2) REFERENCES ActiveSubstances(ActiveSubstancesID)
);

-- Create the ActiveSubstances_SideEffects table
CREATE TABLE ActiveSubstances_SideEffects
(
  SideEffects NVARCHAR(250) NOT NULL,
  ActiveSubstancesID INT NOT NULL,
  PRIMARY KEY (SideEffects, ActiveSubstancesID),
  FOREIGN KEY (ActiveSubstancesID) REFERENCES ActiveSubstances(ActiveSubstancesID)
);

-- Create the Doctor table
CREATE TABLE Doctor
(
  DoctorID INT NOT NULL identity(1,1),
  Specializtion NVARCHAR(50) NOT NULL,
  ClinicID INT NOT NULL,
  SSN BIGINT NOT NULL,
  PRIMARY KEY (DoctorID),
  FOREIGN KEY (ClinicID) REFERENCES Clinic(ClinicID),
  FOREIGN KEY (SSN) REFERENCES HMS_User(SSN)
);

-- Create the Appointment table
CREATE TABLE Appointment
(
  AppointmentID INT NOT NULL identity(1,1),
  AppointmentDate DATE NOT NULL,
  AppointmentStatus CHAR NOT NULL,
  PatientID INT NOT NULL,
  ClinicID INT NOT NULL,
  receptionID INT NOT NULL,
  PRIMARY KEY (AppointmentID),
  FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
  FOREIGN KEY (ClinicID) REFERENCES Clinic(ClinicID),
  FOREIGN KEY (receptionID) REFERENCES Reception(receptionID)
);

-- Create the Prescription table
CREATE TABLE Prescription
(
  PrescriptionID INT NOT NULL identity(1,1),
  Dosage INT NOT NULL,
  DateIssued DATE NOT NULL,
  Duration INT NOT NULL,
  DoctorID INT NOT NULL,
  PharmacyId INT NOT NULL,
  PatientID INT NOT NULL,
  PRIMARY KEY (PrescriptionID),
  FOREIGN KEY (DoctorID) REFERENCES Doctor(DoctorID),
  FOREIGN KEY (PharmacyId) REFERENCES Pharmacy(PharmacyId),
  FOREIGN KEY (PatientID) REFERENCES Patient(PatientID)
);

-- Create the Medication table
CREATE TABLE Medication
(
  MedicationCode NVARCHAR(20) NOT NULL,
  MedName NVARCHAR(50) NOT NULL,
  Strength INT NOT NULL,
  PharmacyId INT NOT NULL,
  PRIMARY KEY (MedicationCode),
  FOREIGN KEY (PharmacyId) REFERENCES Pharmacy(PharmacyId)
);

-- Create the Invoice table
CREATE TABLE Invoice
(
  InvoiceID INT NOT NULL identity(1,1),
  InvoiceDate DATE NOT NULL,
  TotalAmount MONEY NOT NULL,
  PaymentStatus BIT NOT NULL,
  paymenttype CHAR NOT NULL,
  PatientID INT NOT NULL,
  receptionID INT NOT NULL,
  PRIMARY KEY (InvoiceID),
  FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
  FOREIGN KEY (receptionID) REFERENCES Reception(ReceptionID)
);

-- Create the Medications_ActiveSubstance table
CREATE TABLE Medications_ActiveSubstance
(
  ActiveSubstancesID INT NOT NULL,
  MedicationCode NVARCHAR(20) NOT NULL,
  PRIMARY KEY (ActiveSubstancesID, MedicationCode),
  FOREIGN KEY (ActiveSubstancesID) REFERENCES ActiveSubstances(ActiveSubstancesID),
  FOREIGN KEY (MedicationCode) REFERENCES Medication(MedicationCode)
);

-- Create the PrescriptionActiveSubstance table
CREATE TABLE PrescriptionActiveSubstance
(
  PrescriptionID INT NOT NULL,
  ActiveSubstancesID INT NOT NULL,
  PRIMARY KEY (PrescriptionID, ActiveSubstancesID),
  FOREIGN KEY (PrescriptionID) REFERENCES Prescription(PrescriptionID),
  FOREIGN KEY (ActiveSubstancesID) REFERENCES ActiveSubstances(ActiveSubstancesID)
);

-- Create the Patient_Medications table
CREATE TABLE Patient_Medications
(
  DateIssued DATE NOT NULL,
  PatientID INT NOT NULL,
  MedicationCode NVARCHAR(20) NOT NULL,
  PRIMARY KEY (PatientID, MedicationCode),
  FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
  FOREIGN KEY (MedicationCode) REFERENCES Medication(MedicationCode)
);

-- Create the Doctors_AvaiIableAppointments table
CREATE TABLE Doctors_AvaiIableAppointments
(
  DoctorID INT NOT NULL,
  AvaAppDate DATE NOT NULL,
  ShiftNumber TINYINT NOT NULL,
  PRIMARY KEY (DoctorID, AvaAppDate, ShiftNumber),
  FOREIGN KEY (DoctorID) REFERENCES Doctor(DoctorID),
  FOREIGN KEY (AvaAppDate, ShiftNumber) REFERENCES AvailableAppointment(AvaAppDate, ShiftNumber)
);
alter table patient 
add FOREIGN KEY (SSN) REFERENCES HMS_User(SSN)

alter table MedicalRecord 
add DoctorID int Not null  REFERENCES Doctor(DoctorID);



create or alter proc AddAvailableAppoitmentInTableAvailableAppointment @EndDate date
as 
begin 
	--add Available Appoitment in table AvailableAppointment 
	-- Create a CTE to generate dates for the rest of the year excluding Fridays
	WITH DateSeries AS (
		SELECT 
			CAST(GETDATE() AS DATE) AS AvaAppDate
		UNION ALL
		SELECT 
			DATEADD(DAY, 1, AvaAppDate)
		FROM 
			DateSeries
		WHERE 
			AvaAppDate < @EndDate --'2024-12-31'
	)
	-- Filter out Fridays and insert Shift 1 and 2 into AvailableAppointment table
	INSERT INTO AvailableAppointment (AvaAppDate, ShiftNumber)
	SELECT 
		AvaAppDate,
		ShiftNumber
	FROM 
		DateSeries
	CROSS JOIN 
		(SELECT 1 AS ShiftNumber UNION ALL SELECT 2) S
	WHERE 
		DATEPART(WEEKDAY, AvaAppDate) NOT IN (6); -- 6 represents Friday based on SQL Server's default setting
end;


use HMS
-- Insert doctors into the HMS_User table with User_type set to 'D'
INSERT INTO HMS_User (FirstName, LastName, DateOfBirth, Phone, Email, SSN, User_Password, User_type, gender)
VALUES
('John', 'Doe', '1970-01-01', '5550001', 'johndoe1@example.com', 111111111, 'password1', 'D', 'M'),
('Jane', 'Smith', '1972-02-02', '5550002', 'janesmith2@example.com', 111111112, 'password2', 'D', 'F'),
('Robert', 'Johnson', '1975-03-03', '5550003', 'robertjohnson3@example.com', 111111113, 'password3', 'D', 'M'),
('Emily', 'Davis', '1976-04-04', '5550004', 'emilydavis4@example.com', 111111114, 'password4', 'D', 'F'),
('Michael', 'Brown', '1978-05-05', '5550005', 'michaelbrown5@example.com', 111111115, 'password5', 'D', 'M'),
('Sarah', 'Wilson', '1980-06-06', '5550006', 'sarahwilson6@example.com', 111111116, 'password6', 'D', 'F'),
('William', 'Jones', '1982-07-07', '5550007', 'williamjones7@example.com', 111111117, 'password7', 'D', 'M'),
('Jessica', 'Garcia', '1984-08-08', '5550008', 'jessicagarcia8@example.com', 111111118, 'password8', 'D', 'F'),
('David', 'Martinez', '1986-09-09', '5550009', 'davidmartinez9@example.com', 111111119, 'password9', 'D', 'M'),
('Sophia', 'Rodriguez', '1988-10-10', '5550010', 'sophiarodriguez10@example.com', 111111120, 'password10', 'D', 'F'),
('James', 'Lopez', '1990-11-11', '5550011', 'jameslopez11@example.com', 111111121, 'password11', 'D', 'M'),
('Olivia', 'Gonzalez', '1992-12-12', '5550012', 'oliviagonzalez12@example.com', 111111122, 'password12', 'D', 'F'),
('Alexander', 'Hernandez', '1994-01-13', '5550013', 'alexanderhernandez13@example.com', 111111123, 'password13', 'D', 'M'),
('Isabella', 'Perez', '1996-02-14', '5550014', 'isabellaperez14@example.com', 111111124, 'password14', 'D', 'F'),
('Benjamin', 'Thomas', '1998-03-15', '5550015', 'benjaminthomas15@example.com', 111111125, 'password15', 'D', 'M'),
('Ava', 'Moore', '2000-04-16', '5550016', 'avamoore16@example.com', 111111126, 'password16', 'D', 'F'),
('Lucas', 'Taylor', '2002-05-17', '5550017', 'lucastaylor17@example.com', 111111127, 'password17', 'D', 'M'),
('Mia', 'Anderson', '2004-06-18', '5550018', 'miaanderson18@example.com', 111111128, 'password18', 'D', 'F'),
('Mason', 'Lee', '2006-07-19', '5550019', 'masonlee19@example.com', 111111129, 'password19', 'D', 'M'),
('Charlotte', 'Walker', '2008-08-20', '5550020', 'charlottewalker20@example.com', 111111130, 'password20', 'D', 'F'),
('Logan', 'Hall', '2010-09-21', '5550021', 'loganhall21@example.com', 111111131, 'password21', 'D', 'M'),
('Harper', 'Young', '2012-10-22', '5550022', 'harperyoung22@example.com', 111111132, 'password22', 'D', 'F'),
('Elijah', 'King', '2014-11-23', '5550023', 'elijahking23@example.com', 111111133, 'password23', 'D', 'M'),
('Amelia', 'Scott', '2016-12-24', '5550024', 'ameliascott24@example.com', 111111134, 'password24', 'D', 'F'),
('Oliver', 'Green', '2018-01-25', '5550025', 'olivergreen25@example.com', 111111135, 'password25', 'D', 'M'),
('Abigail', 'Adams', '2020-02-26', '5550026', 'abigailadams26@example.com', 111111136, 'password26', 'D', 'F'),
('Henry', 'Nelson', '2022-03-27', '5550027', 'henrynelson27@example.com', 111111137, 'password27', 'D', 'M'),
('Evelyn', 'Carter', '2024-04-28', '5550028', 'evelyncarter28@example.com', 111111138, 'password28', 'D', 'F'),
('Sebastian', 'Mitchell', '2026-05-29', '5550029', 'sebastianmitchell29@example.com', 111111139, 'password29', 'D', 'M'),
('Scarlett', 'Perez', '2028-06-30', '5550030', 'scarlettperez30@example.com', 111111140, 'password30', 'D', 'F'),
('Jack', 'Roberts', '2030-07-31', '5550031', 'jackroberts31@example.com', 111111141, 'password31', 'D', 'M'),
('Avery', 'Turner', '2032-08-01', '5550032', 'averyturner32@example.com', 111111142, 'password32', 'D', 'F'),
('Owen', 'Phillips', '2034-09-02', '5550033', 'owenphillips33@example.com', 111111143, 'password33', 'D', 'M'),
('Lily', 'Campbell', '2036-10-03', '5550034', 'lilycampbell34@example.com', 111111144, 'password34', 'D', 'F'),

--Insert patient into the HMS_User table with User_type set to 'P'

('John', 'Doe', '1985-01-15', '123-456-7890', 'john.doe@example.com'                       , 999999951, 'password123', 'P', 'M'),
('Jane', 'Smith', '1990-02-20', '234-567-8901', 'jane.smith@example.com'                   , 999999952, 'password123', 'P', 'F'),
('Mark', 'Johnson', '1975-03-25', '345-678-9012', 'mark.johnson@example.com'               , 999999953, 'password123', 'P', 'M'),
('Lisa', 'Williams', '1980-04-30', '456-789-0123', 'lisa.williams@example.com'             , 999999954, 'password123', 'P', 'F'),
('Michael', 'Brown', '1995-05-10', '567-890-1234', 'michael.brown@example.com'             , 999999955, 'password123', 'P', 'M'),
('Olivia', 'Moore', '1987-01-10', '111-222-3333', 'olivia.moore@example.com'               , 999999956, 'password123', 'P', 'F'),
('William', 'Martinez', '1990-03-15', '222-333-4444', 'william.martinez@example.com'       , 999999957, 'password123', 'P', 'M'),
('Emma', 'Garcia', '1992-06-25', '333-444-5555', 'emma.garcia@example.com'                 , 999999958, 'password123', 'P', 'F'),
('Alexander', 'Rodriguez', '1985-12-02', '444-555-6666', 'alexander.rodriguez@example.com' , 999999959, 'password123', 'P', 'M'),
('Isabella', 'Martinez', '1998-11-11', '555-666-7777', 'isabella.martinez@example.com'     , 999999960, 'password123', 'P', 'F'),
('Sophia', 'Hernandez', '1989-04-07', '666-777-8888', 'sophia.hernandez@example.com'       , 999999961, 'password123', 'P', 'F'),
('Benjamin', 'Lopez', '1986-02-15', '777-888-9999', 'benjamin.lopez@example.com'           , 999999962, 'password123', 'P', 'M'),
('Mia', 'Gonzalez', '1994-07-23', '888-999-0000', 'mia.gonzalez@example.com'               , 999999963, 'password123', 'P', 'F'),
('James', 'Wilson', '1975-10-29', '999-000-1111', 'james.wilson@example.com'               , 999999964, 'password123', 'P', 'M'),
('Amelia', 'Anderson', '1981-05-18', '000-111-2222', 'amelia.anderson@example.com', 999999965, 'password123', 'P', 'F'),
('Mason', 'Thomas', '1992-09-22', '111-222-3334', 'mason.thomas@example.com', 999999966, 'password123', 'P', 'M'),
('Ava', 'Taylor', '1993-02-14', '222-333-4445', 'ava.taylor@example.com', 999999967, 'password123', 'P', 'F'),
('Logan', 'Lee', '1988-06-19', '333-444-5556', 'logan.lee@example.com', 999999968, 'password123', 'P', 'M'),
('Charlotte', 'White', '1995-03-12', '444-555-6667', 'charlotte.white@example.com', 999999969, 'password123', 'P', 'F'),
('Lucas', 'Harris', '1987-08-30', '555-666-7778', 'lucas.harris@example.com', 999999970, 'password123', 'P', 'M'),
('Harper', 'Clark', '1994-01-05', '666-777-8889', 'harper.clark@example.com', 999999971, 'password123', 'P', 'F'),
('Elijah', 'Young', '1986-11-27', '777-888-9990', 'elijah.young@example.com', 999999972, 'password123', 'P', 'M'),
('Evelyn', 'King', '1980-12-31', '888-999-0001', 'evelyn.king@example.com', 999999973, 'password123', 'P', 'F'),
('Jacob', 'Scott', '1991-04-21', '999-000-1112', 'jacob.scott@example.com', 999999974, 'password123', 'P', 'M'),
('Abigail', 'Green', '1996-09-17', '000-111-2223', 'abigail.green@example.com', 999999975, 'password123', 'P', 'F'),
('Henry', 'Adams', '1982-02-10', '111-222-3335', 'henry.adams@example.com', 999999976, 'password123', 'P', 'M'),
('Grace', 'Baker', '1983-06-22', '222-333-4446', 'grace.baker@example.com', 999999977, 'password123', 'P', 'F'),
('Michael', 'Nelson', '1989-08-16', '333-444-5557', 'michael.nelson@example.com', 999999978, 'password123', 'P', 'M'),
('Lily', 'Carter', '1994-07-09', '444-555-6668', 'lily.carter@example.com', 999999979, 'password123', 'P', 'F'),
('Daniel', 'Mitchell', '1978-10-25', '555-666-7779', 'daniel.mitchell@example.com', 999999980, 'password123', 'P', 'M'),
('Victoria', 'Perez', '1990-01-18', '666-777-8880', 'victoria.perez@example.com', 999999981, 'password123', 'P', 'F'),
('Jack', 'Roberts', '1993-03-30', '777-888-9991', 'jack.roberts@example.com', 999999982, 'password123', 'P', 'M'),
('Elizabeth', 'Turner', '1995-05-24', '888-999-0002', 'elizabeth.turner@example.com', 999999983, 'password123', 'P', 'F'),
('Sebastian', 'Phillips', '1985-08-15', '999-000-1113', 'sebastian.phillips@example.com', 999999984, 'password123', 'P', 'M'),
('Zoey', 'Campbell', '1997-12-01', '000-111-2224', 'zoey.campbell@example.com', 999999985, 'password123', 'P', 'F'),
('Matthew', 'Evans', '1984-04-14', '111-222-3336', 'matthew.evans@example.com', 999999986, 'password123', 'P', 'M'),
('Scarlett', 'Edwards', '1991-02-26', '222-333-4447', 'scarlett.edwards@example.com', 999999987, 'password123', 'P', 'F'),
('David', 'Collins', '1987-06-13', '333-444-5558', 'david.collins@example.com', 999999988, 'password123', 'P', 'M'),
('Riley', 'Stewart', '1994-07-30', '444-555-6669', 'riley.stewart@example.com', 999999989, 'password123', 'P', 'F'),
('Isaac', 'Sanchez', '1979-10-22', '555-666-7770', 'isaac.sanchez@example.com', 999999990, 'password123', 'P', 'M'),
('Aria', 'Morris', '1988-09-09', '666-777-8881', 'aria.morris@example.com', 999999991, 'password123', 'P', 'F'),
('Carter', 'Rogers', '1992-05-05', '777-888-9992', 'carter.rogers@example.com', 999999992, 'password123', 'P', 'M'),
('Penelope', 'Reed', '1990-08-18', '888-999-0003', 'penelope.reed@example.com', 999999993, 'password123', 'P', 'F'),
('Leo', 'Cook', '1985-03-03', '999-000-1114', 'leo.cook@example.com', 999999994, 'password123', 'P', 'M'),
('Hazel', 'Morgan', '1983-10-31', '000-111-2225', 'hazel.morgan@example.com', 999999995, 'password123', 'P', 'F'),
('Jackson', 'Bell', '1997-06-20', '111-222-3337', 'jackson.bell@example.com', 999999996, 'password123', 'P', 'M'),
('Layla', 'Murphy', '1995-11-08', '222-333-4448', 'layla.murphy@example.com', 999999997, 'password123', 'P', 'F'),
('Gabriel', 'Bailey', '1989-02-27', '333-444-5559', 'gabriel.bailey@example.com', 999999998, 'password123', 'P', 'M'),
('Madison', 'Rivera', '1994-04-22', '444-555-6670', 'madison.rivera@example.com', 999999999, 'password123', 'P', 'F'),
('Lucas', 'Flores', '1986-07-14', '555-666-7771', 'lucas.flores@example.com', 9999991000, 'password123', 'P', 'M'),
('Aiden', 'Foster', '1991-11-09', '666-777-8882', 'aiden.foster@example.com', 9999991001, 'password123', 'P', 'M'),
('Nora', 'Powell', '1993-03-04', '777-888-9993', 'nora.powell@example.com', 9999991002, 'password123', 'P', 'F'),
('Oliver', 'Howard', '1987-06-06', '888-999-0004', 'oliver.howard@example.com', 9999991003, 'password123', 'P', 'M'),
('Avery', 'Ward', '1994-07-15', '999-000-1115', 'avery.ward@example.com', 9999991004, 'password123', 'P', 'F'),
('Liam', 'Peterson', '1985-09-28', '000-111-2226', 'liam.peterson@example.com', 9999991005, 'password123', 'P', 'M'),
('Mila', 'Gray', '1992-10-17', '111-222-3338', 'mila.gray@example.com', 9999991006, 'password123', 'P', 'F'),
('Ethan', 'Ramirez', '1990-12-30', '222-333-4449', 'ethan.ramirez@example.com', 9999991007, 'password123', 'P', 'M'),
('Emily', 'James', '1988-04-11', '333-444-5560', 'emily.james@example.com', 9999991008, 'password123', 'P', 'F'),
('Lucas', 'Watson', '1991-06-18', '444-555-6671', 'lucas.watson@example.com', 9999991009, 'password123', 'P', 'M'),
('Sophia', 'Brooks', '1989-02-14', '555-666-7772', 'sophia.brooks@example.com', 9999991010, 'password123', 'P', 'F'),
('Jackson', 'Kelly', '1986-05-19', '666-777-8883', 'jackson.kelly@example.com', 9999991011, 'password123', 'P', 'M'),
('Harper', 'Sanders', '1993-08-08', '777-888-9994', 'harper.sanders@example.com', 9999991012, 'password123', 'P', 'F'),
('Mason', 'Price', '1995-11-22', '888-999-0005', 'mason.price@example.com', 9999991013, 'password123', 'P', 'M'),
('Isabella', 'Perry', '1990-01-27', '999-000-1116', 'isabella.perry@example.com', 9999991014, 'password123', 'P', 'F'),
('Logan', 'Ross', '1987-07-13', '000-111-2227', 'logan.ross@example.com', 9999991015, 'password123', 'P', 'M'),
('Lily', 'Jenkins', '1992-04-06', '111-222-3339', 'lily.jenkins@example.com', 9999991016, 'password123', 'P', 'F'),
('Jacob', 'Stewart', '1985-10-20', '222-333-4450', 'jacob.stewart@example.com', 9999991017, 'password123', 'P', 'M'),
('Ava', 'Reed', '1991-12-18', '333-444-5561', 'ava.reed@example.com', 9999991018, 'password123', 'P', 'F'),
('William', 'Bryant', '1994-09-25', '444-555-6672', 'william.bryant@example.com', 9999991019, 'password123', 'P', 'M'),
('Scarlett', 'Parker', '1996-03-11', '555-666-7773', 'scarlett.parker@example.com', 9999991020, 'password123', 'P', 'F'),
('Benjamin', 'Bennett', '1988-06-29', '666-777-8884', 'benjamin.bennett@example.com', 9999991021, 'password123', 'P', 'M'),
('Chloe', 'Coleman', '1990-08-22', '777-888-9995', 'chloe.coleman@example.com', 9999991022, 'password123', 'P', 'F'),
('Elijah', 'Collins', '1984-04-23', '888-999-0006', 'elijah.collins@example.com', 9999991023, 'password123', 'P', 'M'),
('Mia', 'Richardson', '1989-11-07', '999-000-1117', 'mia.richardson@example.com', 9999991024, 'password123', 'P', 'F'),
('Henry', 'Cox', '1995-02-03', '000-111-2228', 'henry.cox@example.com', 9999991025, 'password123', 'P', 'M'),
('Amelia', 'Ward', '1994-03-14', '111-222-3340', 'amelia.ward@example.com', 9999991026, 'password123', 'P', 'F'),
('Daniel', 'Morgan', '1987-10-27', '222-333-4451', 'daniel.morgan@example.com', 9999991027, 'password123', 'P', 'M'),
('Riley', 'Jenkins', '1993-12-20', '333-444-5562', 'riley.jenkins@example.com', 9999991028, 'password123', 'P', 'F'),
('Alexander', 'Mitchell', '1991-05-09', '444-555-6673', 'alexander.mitchell@example.com', 9999991029, 'password123', 'P', 'M'),
('Madison', 'Hunter', '1992-08-30', '555-666-7774', 'madison.hunter@example.com', 9999991030, 'password123', 'P', 'F'),
('Sebastian', 'Harrison', '1985-01-11', '666-777-8885', 'sebastian.harrison@example.com', 9999991031, 'password123', 'P', 'M'),
('Charlotte', 'Hart', '1988-05-15', '777-888-9996', 'charlotte.hart@example.com', 9999991032, 'password123', 'P', 'F'),
('James', 'Wood', '1989-09-24', '888-999-0007', 'james.wood@example.com', 9999991033, 'password123', 'P', 'M'),
('Emma', 'Sullivan', '1994-06-05', '999-000-1118', 'emma.sullivan@example.com', 9999991034, 'password123', 'P', 'F'),
('Lucas', 'Barnes', '1986-03-18', '000-111-2229', 'lucas.barnes@example.com', 9999991035, 'password123', 'P', 'M'),
('Evelyn', 'Fisher', '1991-04-30', '111-222-3341', 'evelyn.fisher@example.com', 9999991036, 'password123', 'P', 'F'),
('Liam', 'Henderson', '1987-12-28', '222-333-4452', 'liam.henderson@example.com', 9999991037, 'password123', 'P', 'M'),
('Avery', 'Wells', '1990-02-19', '333-444-5563', 'avery.wells@example.com', 9999991038, 'password123', 'P', 'F'),
('Jackson', 'Rice', '1989-07-17', '444-555-6674', 'jackson.rice@example.com', 9999991039, 'password123', 'P', 'M'),
('Sofia', 'Grant', '1995-09-02', '555-666-7775', 'sofia.grant@example.com', 9999991040, 'password123', 'P', 'F'),
('Mason', 'Owens', '1986-11-30', '666-777-8886', 'mason.owens@example.com', 9999991041, 'password123', 'P', 'M'),
('Victoria', 'Ferguson', '1994-03-28', '777-888-9997', 'victoria.ferguson@example.com', 9999991042, 'password123', 'P', 'F'),
('Logan', 'Murray', '1985-08-11', '888-999-0008', 'logan.murray@example.com', 9999991043, 'password123', 'P', 'M'),
('Grace', 'Hill', '1990-12-22', '999-000-1119', 'grace.hill@example.com', 9999991044, 'password123', 'P', 'F'),
('Michael', 'Harper', '1988-06-13', '000-111-2230', 'michael.harper@example.com', 9999991045, 'password123', 'P', 'M'),
('Zoey', 'Hughes', '1992-05-16', '111-222-3342', 'zoey.hughes@example.com', 9999991046, 'password123', 'P', 'F'),
('Caleb', 'Graham', '1989-09-25', '222-333-4453', 'caleb.graham@example.com', 9999991047, 'password123', 'P', 'M'),
('Lillian', 'Clark', '1987-02-07', '333-444-5564', 'lillian.clark@example.com', 9999991048, 'password123', 'P', 'F'),
('Noah', 'Patterson', '1986-04-12', '444-555-6675', 'noah.patterson@example.com', 9999991049, 'password123', 'P', 'M'),
('Ella', 'Brooks', '1995-10-03', '555-666-7776', 'ella.brooks@example.com', 9999991050, 'password123', 'P', 'F'),
-- Insert Nurses into the HMS_User table with User_type set to 'N' 
('Emily'    , 'Johnson'   , '1990-01-01', '1234567800', 'emily.johnson@example.com'      , 222222200, 'password1',  'N', 'F'),
('Michael'  , 'Smith'     , '1990-02-01', '1234567801', 'michael.smith@example.com'      , 222222201, 'password2',  'N', 'M'),
('Sophia'   , 'Williams'  , '1990-03-01', '1234567802', 'sophia.williams@example.com'    , 222222202, 'password3',  'N', 'F'),
('James'    , 'Brown'     , '1990-04-01', '1234567803', 'james.brown@example.com'        , 222222203, 'password4',  'N', 'M'),
('Olivia'   , 'Jones'	  , '1990-05-01', '1234567804', 'olivia.jones@example.com'       , 222222204, 'password5',  'N', 'F'),
('Liam'     , 'Garcia'    , '1990-06-01', '1234567805', 'liam.garcia@example.com'        , 222222205, 'password6',  'N', 'M'),
('Isabella' , 'Martinez'  , '1990-07-01', '1234567806', 'isabella.martinez@example.com'  , 222222206, 'password7',  'N', 'F'),
('Noah'     , 'Davis'     , '1990-08-01', '1234567807', 'noah.davis@example.com'         , 222222207, 'password8',  'N', 'M'),
('Mia'      , 'Lopez'     , '1990-09-01', '1234567808', 'mia.lopez@example.com'          , 222222208, 'password9',  'N', 'F'),
('William'  , 'Gonzalez'  , '1990-10-01', '1234567809', 'william.gonzalez@example.com'   , 222222209, 'password10', 'N', 'M'),
('Ava'      , 'Wilson'    , '1990-11-01', '1234567810', 'ava.wilson@example.com'         , 222222210, 'password11', 'N', 'F'),
('Ethan'    , 'Anderson'  , '1990-12-01', '1234567811', 'ethan.anderson@example.com'     , 222222211, 'password12', 'N', 'M'),
('Charlotte', 'Thomas'    , '1991-01-01', '1234567812', 'charlotte.thomas@example.com'   , 222222212, 'password13', 'N', 'F'),
('Alexander', 'Taylor'    , '1991-02-01', '1234567813', 'alexander.taylor@example.com'   , 222222213, 'password14', 'N', 'M'),
('Amelia'   , 'Moore'     , '1991-03-01', '1234567814', 'amelia.moore@example.com'       , 222222214, 'password15', 'N', 'F'),
('Emily'    , 'Johnson'   , '1991-04-01', '1234567805', 'nurse16@example.com'            , 222222215, 'password16', 'N', 'F'),
('Michael'  , 'Smith'     , '1991-05-01', '1234567806', 'nurse17@example.com'            , 222222216, 'password17', 'N', 'M'),
('Sophia'   , 'Williams'  , '1991-06-01', '1234567807', 'nurse18@example.com'            , 222222217, 'password18', 'N', 'F'),
('James'    , 'Brown'     , '1991-07-01', '1234567808', 'nurse19@example.com'            , 222222218, 'password19', 'N', 'M'),
('Olivia'   , 'Jones'	  , '1991-08-01', '1234567809', 'nurse20@example.com'            , 222222219, 'password20', 'N', 'F'),
('Liam'     , 'Garcia'    , '1991-09-01', '1234567810', 'nurse21@example.com'            , 222222220, 'password21', 'N', 'M'),
('Isabella' , 'Martinez'  , '1991-10-01', '1234567811', 'nurse22@example.com'            , 222222221, 'password22', 'N', 'F'),
('Noah'     , 'Davis'     , '1991-11-01', '1234567812', 'nurse23@example.com'            , 222222222, 'password23', 'N', 'M'),
('Mia'      , 'Lopez'     , '1991-12-01', '1234567813', 'nurse24@example.com'            , 222222223, 'password24', 'N', 'F'),
('William'  , 'Gonzalez'  , '1992-01-01', '1234567814', 'nurse25@example.com'            , 222222224, 'password25', 'N', 'M'),
('Ava'      , 'Wilson'    , '1992-02-01', '1234567815', 'nurse26@example.com'            , 222222225, 'password26', 'N', 'F'),
('Ethan'    , 'Anderson'  , '1992-03-01', '1234567816', 'nurse27@example.com'            , 222222226, 'password27', 'N', 'M'),
('Charlotte', 'Thomas'    , '1992-04-01', '1234567817', 'nurse28@example.com'            , 222222227, 'password28', 'N', 'F'),
('Alexander', 'Taylor'    , '1992-05-01', '1234567818', 'nurse29@example.com'            , 222222228, 'password29', 'N', 'M'),
('Amelia'   , 'Moore'     , '1992-06-01', '1234567819', 'nurse30@example.com'            , 222222229, 'password30', 'N', 'F'),
('Emily'    , 'Johnson'   , '1992-07-01', '1234567820', 'nurse31@example.com'            , 222222230, 'password31', 'N', 'F'),
('Michael'  , 'Smith'     , '1992-08-01', '1234567821', 'nurse32@example.com'            , 222222231, 'password32', 'N', 'M'),
('Sophia'   , 'Williams'  , '1992-09-01', '1234567822', 'nurse33@example.com'            , 222222232, 'password33', 'N', 'F'),
('James'    , 'Brown'     , '1992-10-01', '1234567823', 'nurse34@example.com'            , 222222233, 'password34', 'N', 'M'),
('Olivia'   , 'Jones'	  , '1992-11-01', '1234567824', 'nurse35@example.com'            , 222222234, 'password35', 'N', 'F'),
('Liam'     , 'Garcia'    , '1992-12-01', '1234567825', 'nurse36@example.com'            , 222222235, 'password36', 'N', 'M'),
('Isabella' , 'Martinez'  , '1993-01-01', '1234567826', 'nurse37@example.com'            , 222222236, 'password37', 'N', 'F'),
('Noah'     , 'Davis'     , '1993-02-01', '1234567827', 'nurse38@example.com'            , 222222237, 'password38', 'N', 'M'),
('Mia'      , 'Lopez'     , '1993-03-01', '1234567828', 'nurse39@example.com'            , 222222238, 'password39', 'N', 'F'),
('William'  , 'Gonzalez'  , '1993-04-01', '1234567829', 'nurse40@example.com'            , 222222239, 'password40', 'N', 'M'),
('Ava'      , 'Wilson'    , '1993-05-01', '1234567830', 'nurse41@example.com'            , 222222240, 'password41', 'N', 'F'),
('Ethan'    , 'Anderson'  , '1993-06-01', '1234567831', 'nurse42@example.com'            , 222222241, 'password42', 'N', 'M'),
('Charlotte', 'Thomas'    , '1993-07-01', '1234567832', 'nurse43@example.com'            , 222222242, 'password43', 'N', 'F'),
('Alexander', 'Taylor'    , '1993-08-01', '1234567833', 'nurse44@example.com'            , 222222243, 'password44', 'N', 'M'),
('Amelia'   , 'Moore'     , '1993-09-01', '1234567834', 'nurse45@example.com'            , 222222244, 'password45', 'N', 'F'),
('Emily'    , 'Johnson'   , '1993-10-01', '1234567835', 'nurse46@example.com'            , 222222245, 'password46', 'N', 'F'),
('Michael'  , 'Smith'     , '1993-11-01', '1234567836', 'nurse47@example.com'            , 222222246, 'password47', 'N', 'M'),
('Sophia'   , 'Williams'  , '1993-12-01', '1234567837', 'nurse48@example.com'            , 222222247, 'password48', 'N', 'F'),
('James'    , 'Brown'     , '1994-01-01', '1234567838', 'nurse49@example.com'            , 222222248, 'password49', 'N', 'M'),
('Olivia'   , 'Jones'	  , '1994-02-01', '1234567839', 'nurse50@example.com'            , 222222249, 'password50', 'N', 'F'),
('Liam'     , 'Garcia'    , '1994-03-01', '1234567840', 'nurse51@example.com'            , 222222250, 'password51', 'N', 'M'),
('Isabella' , 'Martinez'  , '1994-04-01', '1234567841', 'nurse52@example.com'            , 222222251, 'password52', 'N', 'F'),
('Noah'     , 'Davis'     , '1994-05-01', '1234567842', 'nurse53@example.com'            , 222222252, 'password53', 'N', 'M'),
('Mia'      , 'Lopez'     , '1994-06-01', '1234567843', 'nurse54@example.com'            , 222222253, 'password54', 'N', 'F'),
('William'  , 'Gonzalez'  , '1994-07-01', '1234567844', 'nurse55@example.com'            , 222222254, 'password55', 'N', 'M'),
('Ava'      , 'Wilson'    , '1994-08-01', '1234567845', 'nurse56@example.com'            , 222222255, 'password56', 'N', 'F'),
('Ethan'    , 'Anderson'  , '1994-09-01', '1234567846', 'nurse57@example.com'            , 222222256, 'password57', 'N', 'M'),
('Charlotte', 'Thomas'    , '1994-10-01', '1234567847', 'nurse58@example.com'            , 222222257, 'password58', 'N', 'F'),
('Alexander', 'Taylor'    , '1994-11-01', '1234567848', 'nurse59@example.com'            , 222222258, 'password59', 'N', 'M'),
('Amelia'   , 'Moore'     , '1994-12-01', '1234567849', 'nurse60@example.com'            , 222222259, 'password60', 'N', 'F'),
('Emily'    , 'Johnson'   , '1995-01-01', '1234567850', 'nurse61@example.com'            , 222222260, 'password61', 'N', 'F'),
('Michael'  , 'Smith'     , '1995-02-01', '1234567851', 'nurse62@example.com'            , 222222261, 'password62', 'N', 'M'),
('Sophia'   , 'Williams'  , '1995-03-01', '1234567852', 'nurse63@example.com'            , 222222262, 'password63', 'N', 'F'),
('James'    , 'Brown'     , '1995-04-01', '1234567853', 'nurse64@example.com'            , 222222263, 'password64', 'N', 'M'),
('Olivia'   , 'Jones'	  , '1995-05-01', '1234567854', 'nurse65@example.com'            , 222222264, 'password65', 'N', 'F'),
('Liam'     , 'Garcia'    , '1995-06-01', '1234567855', 'nurse66@example.com'            , 222222265, 'password66', 'N', 'M'),
('Isabella' , 'Martinez'  , '1995-07-01', '1234567856', 'nurse67@example.com'            , 222222266, 'password67', 'N', 'F'),
('Noah'     , 'Davis'     , '1995-08-01', '1234567857', 'nurse68@example.com'            , 222222267, 'password68', 'N', 'M'),
('Mia'      , 'Lopez'     , '1995-09-01', '1234567858', 'nurse69@example.com'            , 222222268, 'password69', 'N', 'F'),
('William'  , 'Gonzalez'  , '1995-10-01', '1234567859', 'nurse70@example.com'            , 222222269, 'password70', 'N', 'M'),

-- Insert Pharmacist into the HMS_User table with User_type set to 'H' 
('John', 'Doe', '1980-01-15', '555-1230001', 'john.doe@example.com', 211111111, 'password1',           'H', 'M'),
('Jane', 'Smith', '1985-05-25', '555-1230002', 'jane.smith@example.com', 211111112, 'password2',       'H', 'F'),
('Emily', 'Davis', '1990-07-10', '555-1230003', 'emily.davis@example.com', 211111113, 'password3',     'H', 'F'),
('Michael', 'Brown', '1983-03-30', '555-1230004', 'michael.brown@example.com', 211111114, 'password4', 'H', 'M'),
('Sarah', 'Johnson', '1988-12-12', '555-1230005', 'sarah.johnson@example.com', 211111115, 'password5', 'H', 'F'),
('David', 'Wilson', '1992-09-19', '555-1230006', 'david.wilson@example.com', 211111116, 'password6',   'H', 'M'),

-- Inserting Receptionists into HMS_User
('Alice', 'Walker', '1982-02-22', '555-1240001', 'alice.walker@example.com', 311111111, 'password7', 'R', 'F'),
('Chris', 'Martin', '1987-04-17', '555-1240002', 'chris.martin@example.com', 311111112, 'password8', 'R', 'M'),
('Nina', 'Thomas', '1991-08-23', '555-1240003', 'nina.thomas@example.com', 311111113, 'password9', 'R', 'F'),
('Robert', 'Lee', '1984-11-02', '555-1240004', 'robert.lee@example.com', 311111114, 'password10', 'R', 'M'),
('Olivia', 'White', '1986-03-15', '555-1240005', 'olivia.white@example.com', 311111115, 'password11', 'R', 'F'),
('James', 'Taylor', '1990-06-10', '555-1240006', 'james.taylor@example.com', 311111116, 'password12', 'R', 'M');


SET IDENTITY_INSERT Clinic on; 
go 
-- Insert initial data into the Clinic table
INSERT INTO Clinic (ClinicID, ClinicName, Phone, Specialization, price)
VALUES
(1, 'General Medicine Clinic', '5550101', 'General Medicine'	 , 75.00),
(2, 'Pediatrics Clinic'      , '5550102', 'Pediatrics'			 , 80.00),
(3, 'Orthopedic Clinic'      , '5550103', 'Orthopedics'			 , 120.00),
(4, 'Cardiology Clinic'		 , '5550104', 'Cardiology'			 , 150.00),
(5, 'Dermatology Clinic'	 , '5550105', 'Dermatology'			 , 90.00),
(6, 'Neurology Clinic'		 , '5550106', 'Neurology'			 , 140.00),
(7, 'Ophthalmology Clinic'   , '5550107', 'Ophthalmology'		 , 110.00),
(8, 'ENT Clinic'             , '5550108', 'Ear, Nose, and Throat', 85.00),
(9, 'Gastroenterology Clinic', '5550109', 'Gastroenterology'     , 130.00),
(10, 'Oncology Clinic'       , '5550110', 'Oncology'             , 160.00),
(11, 'Rheumatology Clinic'   , '5550111', 'Rheumatology'         , 125.00),
(12, 'Endocrinology Clinic'  , '5550112', 'Endocrinology'        , 130.00),
(13, 'Nephrology Clinic'     , '5550113', 'Nephrology'           , 140.00),
(14, 'Pulmonology Clinic'    , '5550114', 'Pulmonology'          , 135.00),
(15, 'Urology Clinic'        , '5550115', 'Urology'              , 145.00),
(16, 'Psychiatry Clinic'     , '5550116', 'Psychiatry'           , 100.00),
(17, 'Geriatrics Clinic'     , '5550117', 'Geriatrics'           , 95.00);
SET IDENTITY_INSERT Clinic off; 
go 

SET IDENTITY_INSERT Doctor on; 
go								  
-- Insert initial data into the Doctor table
INSERT INTO Doctor (DoctorID, Specializtion, ClinicID, SSN)
VALUES
(1, 'Cardiology', 1, 111111111),
(2, 'Cardiology', 1, 111111112),
(3, 'Dermatology', 2, 111111113),
(4, 'Dermatology', 2, 111111114),
(5, 'Pediatrics', 3, 111111115),
(6, 'Pediatrics', 3, 111111116),
(7, 'Orthopedics', 4, 111111117),
(8, 'Orthopedics', 4, 111111118),
(9, 'Neurology', 5, 111111119),
(10, 'Neurology', 5, 111111120),
(11, 'Rheumatology', 11, 111111121),
(12, 'Rheumatology', 11, 111111122),
(13, 'Endocrinology', 12, 111111123),
(14, 'Endocrinology', 12, 111111124),
(15, 'Nephrology', 13, 111111125),
(16, 'Nephrology', 13, 111111126),
(17, 'Pulmonology', 14, 111111127),
(18, 'Pulmonology', 14, 111111128),
(19, 'Urology', 15, 111111129),
(20, 'Urology', 15, 111111130),
(21, 'Psychiatry', 16, 111111131),
(22, 'Psychiatry', 16, 111111132),
(23, 'Geriatrics', 17, 111111133),
(24, 'Geriatrics', 17, 111111134),
(25, 'Neurology Clinic', 6, 111111135),
(26, 'Neurology Clinic', 6, 111111136),
(27, 'Ophthalmology Clinic', 7, 111111137),
(28, 'Ophthalmology Clinic', 7, 111111138),
(29, 'ENT Clinic', 8, 111111139),
(30, 'ENT Clinic', 8, 111111140),
(31, 'ENT Clinic', 9, 111111141),
(32, 'ENT Clinic', 9, 111111142),
(33, 'ENT Clinic', 10, 111111143),
(34, 'ENT Clinic', 10, 111111144);
SET IDENTITY_INSERT Doctor off; 
go								  

SET IDENTITY_INSERT Nurses on; 
go
-- Insert Nurse into the Nurses table 
INSERT INTO Nurses (NurseID, ClinicID, SSN) 
VALUES 
(1, 1, 222222200),
(2, 2, 222222201),
(3, 3, 222222202),
(4, 4, 222222203),
(5, 5, 222222204),
(6, 6, 222222205),
(7, 7, 222222206),
(8, 8, 222222207),
(9, 9, 222222208),
(10, 10, 222222209),
(11, 11, 222222210),
(12, 12, 222222211),
(13, 13, 222222212),
(14, 14, 222222213),
(15, 15, 222222214),
(16, 16, 222222215),
(17, 17, 222222216),
(18, 1, 222222217),
(19, 2, 222222218),
(20, 3, 222222219),
(21, 4, 222222220),
(22, 5, 222222221),
(23, 6, 222222222),
(24, 7, 222222223),
(25, 8, 222222224),
(26, 9, 222222225),
(27, 10, 222222226),
(28, 11, 222222227),
(29, 12, 222222228),
(30, 13, 222222229),
(31, 14, 222222230),
(32, 15, 222222231),
(33, 16, 222222232),
(34, 17, 222222233),
(35, 1, 222222234),
(36, 2, 222222235),
(37, 3, 222222236),
(38, 4, 222222237),
(39, 5, 222222238),
(40, 6, 222222239),
(41, 7, 222222240),
(42, 8, 222222241),
(43, 9, 222222242),
(44, 10, 222222243),
(45, 11, 222222244),
(46, 12, 222222245),
(47, 13, 222222246),
(48, 14, 222222247),
(49, 15, 222222248),
(50, 16, 222222249),
(51, 17, 222222250),
(52, 1, 222222251),
(53, 2, 222222252),
(54, 3, 222222253),
(55, 4, 222222254),
(56, 5, 222222255),
(57, 6, 222222256),
(58, 7, 222222257),
(59, 8, 222222258),
(60, 9, 222222259),
(61, 10, 222222260),
(62, 11, 222222261),
(63, 12, 222222262),
(64, 13, 222222263),
(65, 14, 222222264),
(66, 15, 222222265),
(67, 16, 222222266),
(68, 17, 222222267),
(69, 1, 222222268),
(70, 2, 222222269);
SET IDENTITY_INSERT Nurses off; 
go

SET IDENTITY_INSERT ActiveSubstances on; 
go
-- Insert 20 records into the ActiveSubstances table
INSERT INTO ActiveSubstances (ActiveSubstancesID, ActiveSubstancesName)
VALUES
(1, 'Acetaminophen'),
(2, 'Ibuprofen'),
(3, 'Aspirin'),
(4, 'Ciprofloxacin'),
(5, 'Amoxicillin'),
(6, 'Metformin'),
(7, 'Lisinopril'),
(8, 'Atorvastatin'),
(9, 'Simvastatin'),
(10, 'Omeprazole'),
(11, 'Hydrochlorothiazide'),
(12, 'Amlodipine'),
(13, 'Albuterol'),
(14, 'Clopidogrel'),
(15, 'Gabapentin'),
(16, 'Furosemide'),
(17, 'Losartan'),
(18, 'Montelukast'),
(19, 'Prednisone'),
(20, 'Warfarin');
SET IDENTITY_INSERT ActiveSubstances off; 
go

-- Insert side effects for the active substances
INSERT INTO ActiveSubstances_SideEffects (ActiveSubstancesID, SideEffects)
VALUES
-- Acetaminophen
(1, 'Nausea'),
(1, 'Rash'),
(1, 'Liver damage'),

-- Ibuprofen
(2, 'Stomach pain'),
(2, 'Heartburn'),
(2, 'Ulcers'),

-- Aspirin
(3, 'Gastrointestinal bleeding'),
(3, 'Bruising'),
(3, 'Ringing in the ears'),

-- Ciprofloxacin
(4, 'Nausea'),
(4, 'Diarrhea'),
(4, 'Tendonitis'),

-- Amoxicillin
(5, 'Diarrhea'),
(5, 'Allergic reaction'),
(5, 'Rash'),

-- Metformin
(6, 'Diarrhea'),
(6, 'Nausea'),
(6, 'Lactic acidosis'),

-- Lisinopril
(7, 'Cough'),
(7, 'Dizziness'),
(7, 'Hyperkalemia'),

-- Atorvastatin
(8, 'Muscle pain'),
(8, 'Liver enzyme abnormalities'),
(8, 'Nausea'),

-- Simvastatin
(9, 'Muscle pain'),
(9, 'Liver enzyme abnormalities'),
(9, 'Digestive problems'),

-- Omeprazole
(10, 'Headache'),
(10, 'Nausea'),
(10, 'Diarrhea'),

-- Hydrochlorothiazide
(11, 'Low potassium levels'),
(11, 'Dizziness'),
(11, 'Dehydration'),

-- Amlodipine
(12, 'Swelling'),
(12, 'Fatigue'),
(12, 'Dizziness'),

-- Albuterol
(13, 'Tremor'),
(13, 'Nervousness'),
(13, 'Headache'),

-- Clopidogrel
(14, 'Bleeding'),
(14, 'Bruising'),
(14, 'Diarrhea'),

-- Gabapentin
(15, 'Drowsiness'),
(15, 'Dizziness'),
(15, 'Fatigue'),

-- Furosemide
(16, 'Electrolyte imbalance'),
(16, 'Dehydration'),
(16, 'Low blood pressure'),

-- Losartan
(17, 'Dizziness'),
(17, 'Fatigue'),
(17, 'Hyperkalemia'),

-- Montelukast
(18, 'Headache'),
(18, 'Abdominal pain'),
(18, 'Cough'),

-- Prednisone
(19, 'Weight gain'),
(19, 'High blood pressure'),
(19, 'Mood swings'),

-- Warfarin
(20, 'Bleeding'),
(20, 'Bruising'),
(20, 'Nausea');

-- Insert interactions between active substances
INSERT INTO ActiveSubstance_Interaction (ActiveSubstanceID1, ActiveSubstanceID2, Interaction)
VALUES
-- Acetaminophen interactions
(1, 8, 'Increased risk of liver damage when taken with Atorvastatin'),
(1, 5, 'Increased risk of rash when combined with Amoxicillin'),

-- Ibuprofen interactions
(2, 3, 'Increased risk of gastrointestinal bleeding with Aspirin'),
(2, 7, 'Reduced effectiveness of Lisinopril when combined with Ibuprofen'),

-- Aspirin interactions
(3, 14, 'Increased risk of bleeding when combined with Clopidogrel'),
(3, 20, 'Increased risk of bleeding when combined with Warfarin'),

-- Ciprofloxacin interactions
(4, 16, 'Increased risk of QT prolongation with Furosemide'),
(4, 19, 'Increased risk of tendonitis with Prednisone'),

-- Amoxicillin interactions
(5, 14, 'Increased risk of gastrointestinal side effects with Clopidogrel'),
(5, 18, 'Reduced effectiveness of Montelukast when combined with Amoxicillin'),

-- Metformin interactions
(6, 11, 'Increased risk of hypoglycemia with Hydrochlorothiazide'),
(6, 19, 'Reduced effectiveness of Metformin when combined with Prednisone'),

-- Lisinopril interactions
(7, 11, 'Increased risk of hyperkalemia with Hydrochlorothiazide'),
(7, 17, 'Increased risk of hyperkalemia when combined with Losartan'),

-- Atorvastatin interactions
(8, 9, 'Increased risk of muscle pain with Simvastatin'),
(8, 20, 'Increased risk of bleeding when combined with Warfarin'),

-- Simvastatin interactions
(9, 11, 'Increased risk of muscle pain with Hydrochlorothiazide'),
(9, 17, 'Increased risk of hyperkalemia when combined with Losartan'),

-- Omeprazole interactions
(10, 14, 'Increased risk of gastrointestinal bleeding with Clopidogrel'),
(10, 16, 'Increased risk of electrolyte imbalance when combined with Furosemide'),

-- Hydrochlorothiazide interactions
(11, 13, 'Increased risk of hypokalemia with Albuterol'),
(11, 16, 'Increased risk of dehydration when combined with Furosemide'),

-- Amlodipine interactions
(12, 14, 'Increased risk of bleeding with Clopidogrel'),
(12, 17, 'Increased risk of hyperkalemia when combined with Losartan'),

-- Albuterol interactions
(13, 15, 'Increased risk of dizziness with Gabapentin'),
(13, 18, 'Increased risk of nervousness when combined with Montelukast'),

-- Clopidogrel interactions
(14, 20, 'Increased risk of bleeding with Warfarin'),
(14, 19, 'Increased risk of bleeding when combined with Prednisone'),

-- Gabapentin interactions
(15, 16, 'Increased risk of drowsiness with Furosemide'),
(15, 12, 'Increased risk of dizziness when combined with Amlodipine'),

-- Furosemide interactions
(16, 19, 'Increased risk of electrolyte imbalance with Prednisone'),
(16, 18, 'Increased risk of dehydration when combined with Montelukast'),

-- Losartan interactions
(17, 19, 'Increased risk of hyperkalemia when combined with Prednisone'),
(17, 20, 'Increased risk of bleeding with Warfarin'),

-- Montelukast interactions
(18, 19, 'Increased risk of mood swings when combined with Prednisone'),
(18, 20, 'Increased risk of bleeding when combined with Warfarin'),

-- Prednisone interactions
(19, 20, 'Increased risk of bleeding when combined with Warfarin');


SET IDENTITY_INSERT Pharmacy on; 
go 
INSERT INTO Pharmacy (PharmacyId, Name, Phone)
VALUES (1, 'Central Pharmacy', '555-1234567');
SET IDENTITY_INSERT Pharmacy off; 
go 

SET IDENTITY_INSERT Pharmacist on; 
go 
INSERT INTO Pharmacist (PharmacistID, PharmacyId, SSN)
VALUES
(1, 1, 211111111),
(2, 1, 211111112),
(3, 1, 211111113),
(4, 1, 211111114),
(5, 1, 211111115),
(6, 1, 211111116);
SET IDENTITY_INSERT Pharmacist off; 
go 

SET IDENTITY_INSERT Reception on; 
go
INSERT INTO Reception (ReceptionID, Phone)
VALUES (1, '555-7654321');
SET IDENTITY_INSERT Reception off; 
go 

SET IDENTITY_INSERT Receptionist on; 
go 
INSERT INTO Receptionist (receptionistID, receptionID, SSN)
VALUES
(1, 1, 311111111),
(2, 1, 311111112),
(3, 1, 311111113),
(4, 1, 311111114),
(5, 1, 311111115),
(6, 1, 311111116);
SET IDENTITY_INSERT Receptionist off; 
go 

-- Inserting the last 20 records into the Medication table
INSERT INTO Medication (MedicationCode, MedName, Strength, PharmacyId)
VALUES
('MEDC001', 'Tylenol', 500, 1),
('MEDC002', 'Advil', 400, 1),
('MEDC003', 'Aspirin', 100, 1),
('MEDC004', 'Cipro', 250, 1),
('MEDC005', 'Amoxil', 500, 1),
('MEDC006', 'Glucophage', 850, 1),
('MEDC007', 'Zestril', 10, 1),
('MEDC008', 'Lipitor', 20, 1),
('MEDC009', 'Zocor', 40, 1),
('MEDC010', 'Prilosec', 20, 1),
('MEDC011', 'Microzide', 25, 1),
('MEDC012', 'Norvasc', 5, 1),
('MEDC013', 'Ventolin', 100, 1),
('MEDC014', 'Plavix', 75, 1),
('MEDC015', 'Neurontin', 300, 1),
('MEDC016', 'Lasix', 40, 1),
('MEDC017', 'Cozaar', 50, 1),
('MEDC018', 'Singulair', 10, 1),
('MEDC019', 'Deltasone', 20, 1),
('MEDC020', 'Coumadin', 5, 1);

-- Inserting records into the Medications_ActiveSubstance table
INSERT INTO Medications_ActiveSubstance (ActiveSubstancesID, MedicationCode)
VALUES
-- Tylenol (Acetaminophen)
(1, 'MEDC001'),

-- Advil (Ibuprofen)
(2, 'MEDC002'),

-- Aspirin
(3, 'MEDC003'),

-- Cipro (Ciprofloxacin)
(4, 'MEDC004'),

-- Amoxil (Amoxicillin)
(5, 'MEDC005'),

-- Glucophage (Metformin)
(6, 'MEDC006'),

-- Zestril (Lisinopril)
(7, 'MEDC007'),

-- Lipitor (Atorvastatin)
(8, 'MEDC008'),

-- Zocor (Simvastatin)
(9, 'MEDC009'),

-- Prilosec (Omeprazole)
(10, 'MEDC010'),

-- Microzide (Hydrochlorothiazide)
(11, 'MEDC011'),

-- Norvasc (Amlodipine)
(12, 'MEDC012'),

-- Ventolin (Albuterol)
(13, 'MEDC013'),

-- Plavix (Clopidogrel)
(14, 'MEDC014'),

-- Neurontin (Gabapentin)
(15, 'MEDC015'),

-- Lasix (Furosemide)
(16, 'MEDC016'),

-- Cozaar (Losartan)
(17, 'MEDC017'),

-- Singulair (Montelukast)
(18, 'MEDC018'),

-- Deltasone (Prednisone)
(19, 'MEDC019'),

-- Coumadin (Warfarin)
(20, 'MEDC020');

SET IDENTITY_INSERT Patient on; 
go 
-- Inserting the last 100 records into the Patient table
INSERT INTO Patient (SSN,PatientID, PatAddress)
VALUES 
(999999951 ,951 , '123 Elm Street, Springfield'),
(999999952 ,952 , '456 Maple Avenue, Springfield'),
(999999953 ,953 , '789 Oak Lane, Shelbyville'),
(999999954 ,954 , '101 Pine Road, Capital City'),
(999999955 ,955 , '202 Birch Drive, Ogdenville'),
(999999956 ,956 , '303 Cedar Street, North Haverbrook'),
(999999957 ,957 , '404 Willow Avenue, Springfield'),
(999999958 ,958 , '505 Aspen Court, Springfield'),
(999999959 ,959 , '606 Cherry Lane, Capital City'),
(999999960 ,960 , '707 Poplar Street, Shelbyville'),
(999999961 ,961 , '808 Magnolia Drive, Ogdenville'),
(999999962 ,962 , '909 Redwood Road, North Haverbrook'),
(999999963 ,963 , '1010 Palm Street, Springfield'),
(999999964 ,964 , '1111 Cypress Avenue, Springfield'),
(999999965 ,965 , '1212 Fir Lane, Shelbyville'),
(999999966 ,966 , '1313 Spruce Road, Capital City'),
(999999967 ,967 , '1414 Dogwood Drive, Ogdenville'),
(999999968 ,968 , '1515 Beech Street, North Haverbrook'),
(999999969 ,969 , '1616 Juniper Avenue, Springfield'),
(999999970 ,970 , '1717 Sycamore Lane, Springfield'),
(999999971 ,971 , '1818 Alder Road, Shelbyville'),
(999999972 ,972 , '1919 Chestnut Street, Capital City'),
(999999973 ,973 , '2020 Elm Drive, Ogdenville'),
(999999974 ,974 , '2121 Maple Avenue, North Haverbrook'),
(999999975 ,975 , '2222 Oak Lane, Springfield'),
(999999976 ,976 , '2323 Pine Road, Springfield'),
(999999977 ,977 , '2424 Birch Drive, Shelbyville'),
(999999978 ,978 , '2525 Cedar Street, Capital City'),
(999999979 ,979 , '2626 Willow Avenue, Ogdenville'),
(999999980 ,980 , '2727 Aspen Court, North Haverbrook'),
(999999981 ,981 , '2828 Cherry Lane, Springfield'),
(999999982 ,982 , '2929 Poplar Street, Springfield'),
(999999983 ,983 , '3030 Magnolia Drive, Shelbyville'),
(999999984 ,984 , '3131 Redwood Road, Capital City'),
(999999985 ,985 , '3232 Palm Street, Ogdenville'),
(999999986 ,986 , '3333 Cypress Avenue, North Haverbrook'),
(999999987 ,987 , '3434 Fir Lane, Springfield'),
(999999988 ,988 , '3535 Spruce Road, Springfield'),
(999999989 ,989 , '3636 Dogwood Drive, Shelbyville'),
(999999990 ,990 , '3737 Beech Street, Capital City'),
(999999991 ,991 , '3838 Juniper Avenue, Ogdenville'),
(999999992 ,992 , '3939 Sycamore Lane, North Haverbrook'),
(999999993 ,993 , '4040 Alder Road, Springfield'),
(999999994 ,994 , '4141 Chestnut Street, Springfield'),
(999999995 ,995 , '4242 Elm Drive, Shelbyville'),
(999999996 ,996 , '4343 Maple Avenue, Capital City'),
(999999997 ,997 , '4444 Oak Lane, Ogdenville'),
(999999998 ,998 , '4545 Pine Road, North Haverbrook'),
(999999999 ,999 , '4646 Birch Drive, Springfield'),
(9999991000,1000, '4747 Cedar Street, Springfield'),
(9999991001,1001, '4848 Willow Avenue, Springfield'),
(9999991002,1002, '4949 Aspen Court, Shelbyville'),
(9999991003,1003, '5050 Cherry Lane, Capital City'),
(9999991004,1004, '5151 Poplar Street, Ogdenville'),
(9999991005,1005, '5252 Magnolia Drive, North Haverbrook'),
(9999991006,1006, '5353 Redwood Road, Springfield'),
(9999991007,1007, '5454 Palm Street, Springfield'),
(9999991008,1008, '5555 Cypress Avenue, Shelbyville'),
(9999991009,1009, '5656 Fir Lane, Capital City'),
(9999991010,1010, '5757 Spruce Road, Ogdenville'),
(9999991011,1011, '5858 Dogwood Drive, North Haverbrook'),
(9999991012,1012, '5959 Beech Street, Springfield'),
(9999991013,1013, '6060 Juniper Avenue, Springfield'),
(9999991014,1014, '6161 Sycamore Lane, Shelbyville'),
(9999991015,1015, '6262 Alder Road, Capital City'),
(9999991016,1016, '6363 Chestnut Street, Ogdenville'),
(9999991017,1017, '6464 Elm Drive, North Haverbrook'),
(9999991018,1018, '6565 Maple Avenue, Springfield'),
(9999991019,1019, '6666 Oak Lane, Springfield'),
(9999991020,1020, '6767 Pine Road, Shelbyville'),
(9999991021,1021, '6868 Birch Drive, Capital City'),
(9999991022,1022, '6969 Cedar Street, Ogdenville'),
(9999991023,1023, '7070 Willow Avenue, North Haverbrook'),
(9999991024,1024, '7171 Aspen Court, Springfield'),
(9999991025,1025, '7272 Cherry Lane, Springfield'),
(9999991026,1026, '7373 Poplar Street, Shelbyville'),
(9999991027,1027, '7474 Magnolia Drive, Capital City'),
(9999991028,1028, '7575 Redwood Road, Ogdenville'),
(9999991029,1029, '7676 Palm Street, North Haverbrook'),
(9999991030,1030, '7777 Cypress Avenue, Springfield'),
(9999991031,1031, '7878 Fir Lane, Springfield'),
(9999991032,1032, '7979 Spruce Road, Shelbyville'),
(9999991033,1033, '8080 Dogwood Drive, Capital City'),
(9999991034,1034, '8181 Beech Street, Ogdenville'),
(9999991035,1035, '8282 Juniper Avenue, North Haverbrook'),
(9999991036,1036, '8383 Sycamore Lane, Springfield'),
(9999991037,1037, '8484 Alder Road, Springfield'),
(9999991038,1038, '8585 Chestnut Street, Shelbyville'),
(9999991039,1039, '8686 Elm Drive, Capital City'),
(9999991040,1040, '8787 Maple Avenue, Ogdenville'),
(9999991041,1041, '8888 Oak Lane, North Haverbrook'),
(9999991042,1042, '8989 Pine Road, Springfield'),
(9999991043,1043, '9090 Birch Drive, Springfield'),
(9999991044,1044, '9191 Cedar Street, Shelbyville'),
(9999991045,1045, '9292 Willow Avenue, Capital City'),
(9999991046,1046, '9393 Aspen Court, Ogdenville'),
(9999991047,1047, '9494 Cherry Lane, North Haverbrook'),
(9999991048,1048, '9595 Poplar Street, Springfield'),
(9999991049,1049, '9696 Magnolia Drive, Springfield'),
(9999991050,1050, '9797 Redwood Road, Shelbyville');
SET IDENTITY_INSERT Patient off; 
go 


INSERT INTO Patient_Medications (DateIssued, PatientID, MedicationCode)
VALUES 
('2024-08-20', 951, 'MEDC001'),
('2024-08-21', 952, 'MEDC002'),
('2024-08-22', 953, 'MEDC003'),
('2024-08-23', 954, 'MEDC004'),
('2024-08-24', 955, 'MEDC005'),
('2024-08-25', 956, 'MEDC006'),
('2024-08-26', 957, 'MEDC007'),
('2024-08-27', 958, 'MEDC008'),
('2024-08-28', 959, 'MEDC009'),
('2024-08-29', 960, 'MEDC010'),
('2024-08-30', 961, 'MEDC011'),
('2024-08-31', 962, 'MEDC012'),
('2024-09-01', 963, 'MEDC013'),
('2024-09-02', 964, 'MEDC014'),
('2024-09-03', 965, 'MEDC015'),
('2024-09-04', 966, 'MEDC016'),
('2024-09-05', 967, 'MEDC017'),
('2024-09-06', 968, 'MEDC018'),
('2024-09-07', 969, 'MEDC019'),
('2024-09-08', 970, 'MEDC020'),
('2024-09-09', 971, 'MEDC001'),
('2024-09-10', 972, 'MEDC002'),
('2024-09-11', 973, 'MEDC003'),
('2024-09-12', 974, 'MEDC004'),
('2024-09-13', 975, 'MEDC005'),
('2024-09-14', 976, 'MEDC006'),
('2024-09-15', 977, 'MEDC007'),
('2024-09-16', 978, 'MEDC008'),
('2024-09-17', 979, 'MEDC009'),
('2024-09-18', 980, 'MEDC010'),
('2024-09-19', 981, 'MEDC011'),
('2024-09-20', 982, 'MEDC012'),
('2024-09-21', 983, 'MEDC013'),
('2024-09-22', 984, 'MEDC014'),
('2024-09-23', 985, 'MEDC015'),
('2024-09-24', 986, 'MEDC016'),
('2024-09-25', 987, 'MEDC017'),
('2024-09-26', 988, 'MEDC018'),
('2024-09-27', 989, 'MEDC019'),
('2024-09-28', 990, 'MEDC020'),
('2024-09-29', 991, 'MEDC001'),
('2024-09-30', 992, 'MEDC002'),
('2024-10-01', 993, 'MEDC003'),
('2024-10-02', 994, 'MEDC004'),
('2024-10-03', 995, 'MEDC005'),
('2024-10-04', 996, 'MEDC006'),
('2024-10-05', 997, 'MEDC007'),
('2024-10-06', 998, 'MEDC008'),
('2024-10-07', 999, 'MEDC009'),
('2024-10-08', 1000, 'MEDC010'),
('2024-10-09', 1001, 'MEDC011'),
('2024-10-10', 1002, 'MEDC012'),
('2024-10-11', 1003, 'MEDC013'),
('2024-10-12', 1004, 'MEDC014'),
('2024-10-13', 1005, 'MEDC015'),
('2024-10-14', 1006, 'MEDC016'),
('2024-10-15', 1007, 'MEDC017'),
('2024-10-16', 1008, 'MEDC018'),
('2024-10-17', 1009, 'MEDC019'),
('2024-10-18', 1010, 'MEDC020'),
('2024-10-19', 1011, 'MEDC001'),
('2024-10-20', 1012, 'MEDC002'),
('2024-10-21', 1013, 'MEDC003'),
('2024-10-22', 1014, 'MEDC004'),
('2024-10-23', 1015, 'MEDC005'),
('2024-10-24', 1016, 'MEDC006'),
('2024-10-25', 1017, 'MEDC007'),
('2024-10-26', 1018, 'MEDC008'),
('2024-10-27', 1019, 'MEDC009'),
('2024-10-28', 1020, 'MEDC010'),
('2024-10-29', 1021, 'MEDC011'),
('2024-10-30', 1022, 'MEDC012'),
('2024-10-31', 1023, 'MEDC013'),
('2024-11-01', 1024, 'MEDC014'),
('2024-11-02', 1025, 'MEDC015'),
('2024-11-03', 1026, 'MEDC016'),
('2024-11-04', 1027, 'MEDC017'),
('2024-11-05', 1028, 'MEDC018'),
('2024-11-06', 1029, 'MEDC019'),
('2024-11-07', 1030, 'MEDC020'),
('2024-11-08', 1031, 'MEDC001'),
('2024-11-09', 1032, 'MEDC002'),
('2024-11-10', 1033, 'MEDC003'),
('2024-11-11', 1034, 'MEDC004'),
('2024-11-12', 1035, 'MEDC005'),
('2024-11-13', 1036, 'MEDC006'),
('2024-11-14', 1037, 'MEDC007'),
('2024-11-15', 1038, 'MEDC008'),
('2024-11-16', 1039, 'MEDC009'),
('2024-11-17', 1040, 'MEDC010'),
('2024-11-18', 1041, 'MEDC011'),
('2024-11-19', 1042, 'MEDC012'),
('2024-11-20', 1043, 'MEDC013'),
('2024-11-21', 1044, 'MEDC014'),
('2024-11-22', 1045, 'MEDC015'),
('2024-11-23', 1046, 'MEDC016'),
('2024-11-24', 1047, 'MEDC017'),
('2024-11-25', 1048, 'MEDC018'),
('2024-11-26', 1049, 'MEDC019'),
('2024-11-27', 1050, 'MEDC020');

-- Patient is allergic to Acetaminophen (ActiveSubstancesID 1)
INSERT INTO Patient_ActiveSubstances_Allergies (PatientID, ActiveSubstancesID)
VALUES
(991, 1),
(992, 2),
(993, 3),
(994, 4),
(995, 5),
(996, 6),
(997, 7),
(998, 8),
(999, 9),
(1000, 10),
(1001, 11),
(1002, 12),
(1003, 13),
(1004, 14),
(1005, 15),
(1006, 16),
(1007, 17),
(1008, 18),
(1009, 19),
(1010, 20);

-- Inserting 70 records into the Prescription table
SET IDENTITY_INSERT Prescription on; 
go
INSERT INTO Prescription (PrescriptionID, Dosage, DateIssued, Duration, DoctorID, PharmacyId, PatientID)
VALUES
(1, 500, '2024-01-01', 10,  11, 1, 951),
(2, 200, '2024-01-03', 7,   12, 1, 952),
(3, 150, '2024-01-05', 14,  13, 1, 953),
(4, 250, '2024-01-07', 5,   14, 1, 954),
(5, 300, '2024-01-09', 10,  15, 1, 955),
(6, 100, '2024-01-11', 7,   16, 1, 956),
(7, 200, '2024-01-13', 14,  17, 1, 957),
(8, 500, '2024-01-15', 10,  8, 1, 958),
(9, 150, '2024-01-17', 5,   9, 1, 959),
(10, 300, '2024-01-19', 7,  10, 1, 960),
(11, 200, '2024-01-21', 14, 11, 1, 961),
(12, 250, '2024-01-23', 10, 12, 1, 962),
(13, 100, '2024-01-25', 7,  13, 1, 963),
(14, 500, '2024-01-27', 5,  14, 1, 964),
(15, 300, '2024-01-29', 14, 15, 1, 965),
(16, 200, '2024-01-31', 10, 16, 1, 966),
(17, 250, '2024-02-02', 7,  17, 1, 967),
(18, 150, '2024-02-04', 5,  1, 1, 968),
(19, 100, '2024-02-06', 14, 6, 1, 969),
(20, 300, '2024-02-08', 10, 10, 1, 970),
(21, 500, '2024-02-10', 7,  11, 1, 971),
(22, 200, '2024-02-12', 14, 12, 1, 972),
(23, 250, '2024-02-14', 10, 13, 1, 973),
(24, 150, '2024-02-16', 7,  14, 1, 974),
(25, 300, '2024-02-18', 5,  15, 1, 975),
(26, 100, '2024-02-20', 14, 16, 1, 976),
(27, 200, '2024-02-22', 10, 17, 1, 977),
(28, 500, '2024-02-24', 7,  5, 1, 978),
(29, 250, '2024-02-26', 5,  5, 1, 979),
(30, 150, '2024-02-28', 14, 10, 1, 980),
(31, 300, '2024-03-02', 10, 11, 1, 981),
(32, 100, '2024-03-04', 7,  12, 1, 982),
(33, 200, '2024-03-06', 5,  13, 1, 983),
(34, 500, '2024-03-08', 14, 14, 1, 984),
(35, 300, '2024-03-10', 10, 15, 1, 985),
(36, 150, '2024-03-12', 7,  16, 1, 986),
(37, 200, '2024-03-14', 5,  17, 1, 987),
(38, 100, '2024-03-16', 14, 5, 1, 988),
(39, 250, '2024-03-18', 10, 5, 1, 989),
(40, 300, '2024-03-20',  7, 10, 1, 990),
(41, 200, '2024-03-22',  5, 11, 1, 991),
(42, 500, '2024-03-24', 14, 12, 1, 992),
(43, 150, '2024-03-26', 10, 13, 1, 993),
(44, 100, '2024-03-28',  7, 14, 1, 994),
(45, 250, '2024-03-30',  5, 15, 1, 995),
(46, 200, '2024-04-01', 14, 16, 1, 996),
(47, 300, '2024-04-03', 10, 17, 1, 997),
(48, 500, '2024-04-05',  7, 5, 1, 998),
(49, 250, '2024-04-07',  5, 5, 1, 999),
(50, 150, '2024-04-09', 14, 10, 1, 1000),
(51, 200, '2024-04-11', 10, 11, 1, 1001),
(52, 300, '2024-04-13',  7, 12, 1, 1002),
(53, 500, '2024-04-15',  5, 13, 1, 1003),
(54, 100, '2024-04-17', 14, 14, 1, 1004),
(55, 200, '2024-04-19', 10, 15, 1, 1005),
(56, 150, '2024-04-21',  7, 16, 1, 1006),
(57, 300, '2024-04-23',  5, 17, 1, 1007),
(58, 500, '2024-04-25', 14, 5, 1, 1008),
(59, 250, '2024-04-27', 10, 5, 1, 1009),
(60, 100, '2024-04-29',  7, 10, 1, 1010),
(61, 200, '2024-05-01',  5, 11, 1, 1011),
(62, 150, '2024-05-03', 14, 12, 1, 1012),
(63, 500, '2024-05-05', 10, 13, 1, 1013),
(64, 100, '2024-05-07',  7, 14, 1, 1014),
(65, 300, '2024-05-09',  5, 15, 1, 1015),
(66, 250, '2024-05-11', 14, 16, 1, 1016),
(67, 150, '2024-05-13', 10, 17, 1, 1017),
(68, 200, '2024-05-15',  7, 5, 1, 1018),
(69, 100, '2024-05-17',  5, 5, 1, 1019),
(70, 300, '2024-05-19', 14, 10, 1, 1020);
SET IDENTITY_INSERT Prescription off; 
go

SET IDENTITY_INSERT MedicalRecord on; 
go 
-- Inserting 130 records into the MedicalRecord table
INSERT INTO MedicalRecord ([RecordID], [Diagnosis], [CreatedDate], [LabResults], [PatientID], [DoctorID])
VALUES
(1, 'Hypertension', '2024-01-05', 'Blood pressure: 145/90 mmHg', 951, 1),
(2, 'Diabetes Type 2', '2024-01-10', 'Blood glucose: 180 mg/dL', 952, 2),
(3, 'Asthma', '2024-01-15', 'Spirometry: Reduced FEV1', 953, 3),
(4, 'Hyperlipidemia', '2024-01-20', 'Cholesterol: 250 mg/dL', 954, 4),
(5, 'Migraine', '2024-01-25', 'MRI: Normal', 955, 5),
(6, 'Arthritis', '2024-01-30', 'X-ray: Joint space narrowing', 956, 6),
(7, 'Hypertension', '2024-02-04', 'Blood pressure: 150/95 mmHg', 957, 7),
(8, 'Anemia', '2024-02-09', 'Hemoglobin: 10 g/dL', 958, 8),
(9, 'Bronchitis', '2024-02-14', 'Chest X-ray: Normal', 959, 9),
(10, 'Allergic Rhinitis', '2024-02-19', 'IgE: Elevated', 960, 10),
(11, 'Depression', '2024-02-24', 'PHQ-9: Moderate', 961, 11),
(12, 'Gastroesophageal Reflux Disease', '2024-03-01', 'Endoscopy: Mild esophagitis', 962, 12),
(13, 'Osteoporosis', '2024-03-06', 'DEXA: Reduced bone density', 963, 13),
(14, 'Hypothyroidism', '2024-03-11', 'TSH: Elevated', 964, 14),
(15, 'Chronic Kidney Disease', '2024-03-16', 'eGFR: 45 mL/min', 965, 15),
(16, 'Psoriasis', '2024-03-21', 'Skin biopsy: Consistent with psoriasis', 966, 16),
(17, 'Hypertension', '2024-03-26', 'Blood pressure: 140/85 mmHg', 967, 17),
(18, 'COPD', '2024-03-31', 'Spirometry: Obstructive pattern', 968, 1),
(19, 'Rheumatoid Arthritis', '2024-04-05', 'RF: Positive', 969, 2),
(20, 'Chronic Sinusitis', '2024-04-10', 'CT Sinus: Mucosal thickening', 970, 3),
(21, 'Hypertension', '2024-04-15', 'Blood pressure: 135/80 mmHg', 971, 4),
(22, 'Anxiety', '2024-04-20', 'GAD-7: Mild', 972, 5),
(23, 'Chronic Bronchitis', '2024-04-25', 'Chest X-ray: Hyperinflation', 973, 6),
(24, 'Hypertension', '2024-04-30', 'Blood pressure: 145/90 mmHg', 974, 7),
(25, 'Gout', '2024-05-05', 'Uric acid: Elevated', 975, 8),
(26, 'Osteoarthritis', '2024-05-10', 'X-ray: Joint space narrowing', 976, 9),
(27, 'Hypertension', '2024-05-15', 'Blood pressure: 150/95 mmHg', 977, 10),
(28, 'Chronic Hepatitis C', '2024-05-20', 'HCV RNA: Positive', 978, 11),
(29, 'Hypoglycemia', '2024-05-25', 'Blood glucose: 60 mg/dL', 979, 12),
(30, 'Chronic Pain', '2024-05-30', 'Pain scale: 7/10', 980, 13),
(31, 'Hypertension', '2024-06-04', 'Blood pressure: 140/90 mmHg', 981, 14),
(32, 'Asthma', '2024-06-09', 'Spirometry: Reduced FEV1', 982, 15),
(33, 'Diabetes Type 2', '2024-06-14', 'HbA1c: 7.5%', 983, 16),
(34, 'Hyperthyroidism', '2024-06-19', 'TSH: Low', 984, 17),
(35, 'Chronic Back Pain', '2024-06-24', 'MRI: Disc bulge', 985, 1),
(36, 'Allergic Rhinitis', '2024-06-29', 'IgE: Elevated', 986, 2),
(37, 'Hypertension', '2024-07-04', 'Blood pressure: 135/85 mmHg', 987, 3),
(38, 'Anemia', '2024-07-09', 'Hemoglobin: 11 g/dL', 988, 4),
(39, 'Osteoporosis', '2024-07-14', 'DEXA: Reduced bone density', 989, 5),
(40, 'Hyperlipidemia', '2024-07-19', 'Cholesterol: 240 mg/dL', 990, 6),
(41, 'Gout', '2024-07-24', 'Uric acid: Elevated', 991, 7),
(42, 'Asthma', '2024-07-29', 'Spirometry: Reduced FEV1', 992, 8),
(43, 'Hypothyroidism', '2024-08-03', 'TSH: Elevated', 993, 9),
(44, 'Hypertension', '2024-08-08', 'Blood pressure: 140/85 mmHg', 994, 10),
(45, 'Chronic Kidney Disease', '2024-08-13', 'eGFR: 50 mL/min', 995, 11),
(46, 'Psoriasis', '2024-08-18', 'Skin biopsy: Consistent with psoriasis', 996, 12),
(47, 'Diabetes Type 1', '2024-08-23', 'Blood glucose: 150 mg/dL', 997, 13),
(48, 'Asthma', '2024-08-28', 'Spirometry: Reduced FEV1', 998, 14),
(49, 'Hypertension', '2024-09-02', 'Blood pressure: 135/85 mmHg', 999, 15),
(50, 'Rheumatoid Arthritis', '2024-09-07', 'RF: Positive', 1000, 16),
(51, 'Osteoarthritis', '2024-09-12', 'X-ray: Joint space narrowing', 1001, 17),
(52, 'Hyperlipidemia', '2024-09-17', 'Cholesterol: 230 mg/dL', 1002, 1),
(53, 'Hypothyroidism', '2024-09-22', 'TSH: Elevated', 1003, 2),
(54, 'Chronic Sinusitis', '2024-09-27', 'CT Sinus: Mucosal thickening', 1004, 3),
(55, 'Hypertension', '2024-10-02', 'Blood pressure: 140/90 mmHg', 1005, 4),
(56, 'Anxiety', '2024-10-07', 'GAD-7: Moderate', 1006, 5),
(57, 'Diabetes Type 2', '2024-10-12', 'HbA1c: 7.8%', 1007, 6),
(58, 'Hyperthyroidism', '2024-10-17', 'TSH: Low', 1008, 7),
(59, 'Hypertension', '2024-10-22', 'Blood pressure: 145/95 mmHg', 1009, 8),
(60, 'Asthma', '2024-10-27', 'Spirometry: Reduced FEV1', 1010, 9),
(61, 'Chronic Bronchitis', '2024-11-01', 'Chest X-ray: Hyperinflation', 1011, 10),
(62, 'Hypertension', '2024-11-06', 'Blood pressure: 150/100 mmHg', 1012, 11),
(63, 'Gout', '2024-11-11', 'Uric acid: Elevated', 1013, 12),
(64, 'Anemia', '2024-11-16', 'Hemoglobin: 9 g/dL', 1014, 13),
(65, 'Migraine', '2024-11-21', 'MRI: Normal', 1015, 14),
(66, 'Chronic Pain', '2024-11-26', 'Pain scale: 6/10', 1016, 15),
(67, 'Hypertension', '2024-12-01', 'Blood pressure: 140/90 mmHg', 1017, 16),
(68, 'Osteoarthritis', '2024-12-06', 'X-ray: Joint space narrowing', 1018, 17),
(69, 'Hyperlipidemia', '2024-12-11', 'Cholesterol: 245 mg/dL', 1019, 1),
(70, 'Hypothyroidism', '2024-12-16', 'TSH: Elevated', 1020, 2),
(71, 'Chronic Back Pain', '2024-12-21', 'MRI: Disc bulge', 1021, 3),
(72, 'Gastroesophageal Reflux Disease', '2024-12-26', 'Endoscopy: Mild esophagitis', 1022, 4),
(73, 'Chronic Kidney Disease', '2024-12-31', 'eGFR: 48 mL/min', 1023, 5),
(74, 'Osteoporosis', '2025-01-05', 'DEXA: Reduced bone density', 1024, 6),
(75, 'Asthma', '2025-01-10', 'Spirometry: Reduced FEV1', 1025, 7),
(76, 'Diabetes Type 2', '2025-01-15', 'HbA1c: 8.0%', 1026, 8),
(77, 'Hypertension', '2025-01-20', 'Blood pressure: 145/95 mmHg', 1027, 9),
(78, 'Chronic Pain', '2025-01-25', 'Pain scale: 7/10', 1028, 10),
(79, 'Chronic Sinusitis', '2025-01-30', 'CT Sinus: Mucosal thickening', 1029, 11),
(80, 'Psoriasis', '2025-02-04', 'Skin biopsy: Consistent with psoriasis', 1030, 12),
(81, 'Hypothyroidism', '2025-02-09', 'TSH: Elevated', 1031, 13),
(82, 'Chronic Bronchitis', '2025-02-14', 'Chest X-ray: Normal', 1032, 14),
(83, 'Hypertension', '2025-02-19', 'Blood pressure: 140/90 mmHg', 1033, 15),
(84, 'Gastroesophageal Reflux Disease', '2025-02-24', 'Endoscopy: Mild esophagitis', 1034, 16),
(85, 'Osteoarthritis', '2025-03-01', 'X-ray: Joint space narrowing', 1035, 17),
(86, 'Hyperlipidemia', '2025-03-06', 'Cholesterol: 250 mg/dL', 1036, 1),
(87, 'Hypertension', '2025-03-11', 'Blood pressure: 135/85 mmHg', 1037, 2),
(88, 'Anxiety', '2025-03-16', 'GAD-7: Mild', 1038, 3),
(89, 'Hypoglycemia', '2025-03-21', 'Blood glucose: 65 mg/dL', 1039, 4),
(90, 'Diabetes Type 2', '2025-03-26', 'HbA1c: 7.7%', 1040, 5),
(91, 'Chronic Back Pain', '2025-03-31', 'MRI: Disc bulge', 1041, 6),
(92, 'Hypertension', '2025-04-05', 'Blood pressure: 145/95 mmHg', 1042, 7),
(93, 'Asthma', '2025-04-10', 'Spirometry: Reduced FEV1', 1043, 8),
(94, 'Hypothyroidism', '2025-04-15', 'TSH: Elevated', 1044, 9),
(95, 'Psoriasis', '2025-04-20', 'Skin biopsy: Consistent with psoriasis', 1045, 10),
(96, 'Hyperlipidemia', '2025-04-25', 'Cholesterol: 240 mg/dL', 1046, 11),
(97, 'Osteoporosis', '2025-04-30', 'DEXA: Reduced bone density', 1047, 12),
(98, 'Hypertension', '2025-05-05', 'Blood pressure: 140/85 mmHg', 1048, 13),
(99, 'Chronic Bronchitis', '2025-05-10', 'Chest X-ray: Normal', 1049, 14),
(100, 'Gout', '2025-05-15', 'Uric acid: Elevated', 1050, 15),
(101, 'Chronic Pain', '2025-05-20', 'Pain scale: 7/10', 1041, 16),
(102, 'Hypertension', '2025-05-25', 'Blood pressure: 150/95 mmHg', 1042, 17),
(103, 'Diabetes Type 1', '2025-05-30', 'Blood glucose: 160 mg/dL', 1043, 1),
(104, 'Asthma', '2025-06-04', 'Spirometry: Reduced FEV1', 1044, 2),
(105, 'Hypothyroidism', '2025-06-09', 'TSH: Elevated', 1045, 3),
(106, 'Chronic Kidney Disease', '2025-06-14', 'eGFR: 45 mL/min', 956, 4),
(107, 'Osteoarthritis', '2025-06-19', 'X-ray: Joint space narrowing', 957, 5),
(108, 'Psoriasis', '2025-06-24', 'Skin biopsy: Consistent with psoriasis', 1008, 6),
(109, 'Gout', '2025-06-29', 'Uric acid: Elevated', 959, 7),
(110, 'Hypertension', '2025-07-04', 'Blood pressure: 140/90 mmHg', 960, 8),
(111, 'Anxiety', '2025-07-09', 'GAD-7: Moderate', 961, 9),
(112, 'Hyperlipidemia', '2025-07-14', 'Cholesterol: 245 mg/dL', 962, 10),
(113, 'Osteoporosis', '2025-07-19', 'DEXA: Reduced bone density', 1013, 11),
(114, 'Asthma', '2025-07-24', 'Spirometry: Reduced FEV1', 1014, 12),
(115, 'Hypertension', '2025-07-29', 'Blood pressure: 150/95 mmHg', 1015, 13),
(116, 'Chronic Pain', '2025-08-03', 'Pain scale: 6/10', 1016, 14),
(117, 'Diabetes Type 2', '2025-08-08', 'HbA1c: 7.9%', 1017, 15),
(118, 'Hypothyroidism', '2025-08-13', 'TSH: Elevated', 1018, 16),
(119, 'Chronic Sinusitis', '2025-08-18', 'CT Sinus: Mucosal thickening', 1036, 17),
(120, 'Hyperlipidemia', '2025-08-23', 'Cholesterol: 250 mg/dL', 1010, 1),
(121, 'Asthma', '2025-08-28', 'Spirometry: Reduced FEV1', 1011, 2),
(122, 'Chronic Kidney Disease', '2025-09-02', 'eGFR: 47 mL/min', 1002, 3),
(123, 'Hypothyroidism', '2025-09-07', 'TSH: Elevated', 1003, 4),
(124, 'Osteoarthritis', '2025-09-12', 'X-ray: Joint space narrowing', 1004, 5),
(125, 'Hypertension', '2025-09-17', 'Blood pressure: 135/85 mmHg', 1005, 6),
(126, 'Chronic Bronchitis', '2025-09-22', 'Chest X-ray: Normal', 1006, 7),
(127, 'Gout', '2025-09-27', 'Uric acid: Elevated', 1037, 8),
(128, 'Chronic Pain', '2025-10-02', 'Pain scale: 7/10', 1048, 9),
(129, 'Hypertension', '2025-10-07', 'Blood pressure: 140/90 mmHg', 1029, 10),
(130, 'Psoriasis', '2025-10-12', 'Skin biopsy: Consistent with psoriasis', 1050, 11);
SET IDENTITY_INSERT MedicalRecord off; 
go 

INSERT INTO PrescriptionActiveSubstance (PrescriptionID, ActiveSubstancesID)
VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 5),
(3, 6),
(4, 7),
(4, 8),
(5, 9),
(5, 10),
(6, 11),
(6, 12),
(7, 13),
(7, 14),
(8, 15),
(8, 16),
(9, 17),
(9, 18),
(10, 19),
(10, 20),
(11, 1),
(11, 2),
(12, 3),
(12, 4),
(13, 5),
(13, 6),
(14, 7),
(14, 8),
(15, 9),
(15, 10),
(16, 11),
(16, 12),
(17, 13),
(17, 14),
(18, 15),
(18, 16),
(19, 17),
(19, 18),
(20, 19),
(20, 20),
(21, 1),
(21, 2),
(22, 3),
(22, 4),
(23, 5),
(23, 6),
(24, 7),
(24, 8),
(25, 9),
(25, 10),
(26, 11),
(26, 12),
(27, 13),
(27, 14),
(28, 15),
(28, 16),
(29, 17),
(29, 18),
(30, 19),
(30, 20),
(31, 1),
(31, 2),
(32, 3),
(32, 4),
(33, 5),
(33, 6),
(34, 7),
(34, 8),
(35, 9),
(35, 10),
(36, 11),
(36, 12),
(37, 13),
(37, 14),
(38, 15),
(38, 16),
(39, 17),
(39, 18),
(40, 19),
(40, 20),
(41, 1),
(41, 2),
(42, 3),
(42, 4),
(43, 5),
(43, 6),
(44, 7),
(44, 8),
(45, 9),
(45, 10),
(46, 11),
(46, 12),
(47, 13),
(47, 14),
(48, 15),
(48, 16),
(49, 17),
(49, 18),
(50, 19),
(50, 20),
(51, 1),
(51, 2),
(52, 3),
(52, 4),
(53, 5),
(53, 6),
(54, 7),
(54, 8),
(55, 9),
(55, 10),
(56, 11),
(56, 12),
(57, 13),
(57, 14),
(58, 15),
(58, 16),
(59, 17),
(59, 18),
(60, 19),
(60, 20),
(61, 1),
(61, 2),
(62, 3),
(62, 4),
(63, 5),
(63, 6),
(64, 7),
(64, 8),
(65, 9),
(65, 10),
(66, 11),
(66, 12),
(67, 13),
(67, 14),
(68, 15),
(68, 16),
(69, 17),
(69, 18),
(70, 19),
(70, 20),
(1, 3),
(1, 4),
(2, 5),
(2, 6),
(3, 7),
(3, 8),
(4, 9),
(4, 10),
(5, 11),
(5, 12),
(6, 13),
(6, 14),
(7, 15),
(7, 16),
(8, 17),
(8, 18),
(9, 19),
(9, 20),
(10, 1),
(10, 2),
(11, 3),
(11, 4),
(12, 5),
(12, 6),
(13, 7),
(13, 8),
(14, 9),
(14, 10),
(15, 11),
(15, 12),
(16, 13),
(16, 14),
(17, 15),
(17, 16),
(18, 17),
(18, 18),
(19, 19),
(19, 20),
(20, 1),
(20, 2),
(21, 3),
(21, 4),
(22, 5),
(22, 6),
(23, 7),
(23, 8),
(24, 9),
(24, 10),
(25, 11),
(25, 12),
(26, 13),
(26, 14),
(27, 15),
(27, 16),
(28, 17),
(28, 18),
(29, 19),
(29, 20),
(30, 1),
(30, 2),
(31, 3),
(31, 4),
(32, 5),
(32, 6),
(33, 7),
(33, 8),
(34, 9),
(34, 10),
(35, 11),
(35, 12),
(36, 13),
(36, 14),
(37, 15),
(37, 16),
(38, 17),
(38, 18),
(39, 19),
(39, 20),
(40, 1),
(40, 2),
(41, 3),
(41, 4),
(42, 5),
(42, 6),
(43, 7),
(43, 8),
(44, 9),
(44, 10),
(45, 11),
(45, 12),
(46, 13),
(46, 14),
(47, 15),
(47, 16),
(48, 17),
(48, 18),
(49, 19),
(49, 20),
(50, 1),
(50, 2),
(51, 3),
(51, 4),
(52, 5),
(52, 6),
(53, 7),
(53, 8),
(54, 9),
(54, 10),
(55, 11),
(55, 12),
(56, 13),
(56, 14),
(57, 15),
(57, 16),
(58, 17),
(58, 18),
(59, 19),
(59, 20),
(60, 1),
(60, 2),
(61, 3),
(61, 4),
(62, 5),
(62, 6),
(63, 7),
(63, 8),
(64, 9),
(64, 10),
(65, 11),
(65, 12),
(66, 13),
(66, 14),
(67, 15),
(67, 16),
(68, 17),
(68, 18),
(69, 19),
(69, 20),
(70, 1),
(70, 2);

SET IDENTITY_INSERT Invoice on; 
go
-- Inserting 130 records into the Invoice table
INSERT INTO Invoice (InvoiceID, InvoiceDate, TotalAmount, PaymentStatus, paymenttype, PatientID, receptionID)
VALUES
(1, '2023-01-10', 250.50, 1 , 'O', 960, 1),
(2, '2023-01-12', 300.00, 0 , 'C', 951, 1),
(3, '2023-01-15', 150.75, 1 , 'O', 952, 1),
(4, '2023-01-18', 500.20, 0 , 'C', 953, 1),
(5, '2023-01-20', 220.00, 1 , 'O', 954, 1),
(6, '2023-01-22', 180.45, 0 , 'C', 955, 1),
(7, '2023-01-25', 320.10, 1 , 'O', 956, 1),
(8, '2023-01-28', 440.50, 0 , 'C', 957, 1),
(9, '2023-01-30', 290.75, 1 , 'O', 958, 1),
(10, '2023-02-02', 310.30,0 , 'C', 959, 1),
(11, '2023-02-05', 275.00,1 , 'O', 960, 1),
(12, '2023-02-08', 165.25,0 , 'C', 961, 1),
(13, '2023-02-10', 245.00,1 , 'O', 962, 1),
(14, '2023-02-12', 305.45,0 , 'C', 963, 1),
(15, '2023-02-15', 190.75,1 , 'O', 964, 1),
(16, '2023-02-18', 220.00,0 , 'C', 965, 1),
(17, '2023-02-20', 330.50,1 , 'O', 966, 1),
(18, '2023-02-22', 450.30,0 , 'C', 967, 1),
(19, '2023-02-25', 295.00,1 , 'O', 968, 1),
(20, '2023-02-28', 310.50,0 , 'C', 969, 1),
(21, '2023-03-02', 250.75,1 , 'O', 970, 1),
(22, '2023-03-05', 300.20,0 , 'C', 971, 1),
(23, '2023-03-08', 175.00,1 , 'O', 972, 1),
(24, '2023-03-10', 450.45,0 , 'C', 973, 1),
(25, '2023-03-12', 310.75,1 , 'O', 974, 1),
(26, '2023-03-15', 290.20,0 , 'C', 975, 1),
(27, '2023-03-18', 330.00,1 , 'O', 976, 1),
(28, '2023-03-20', 420.45,0 , 'C', 977, 1),
(29, '2023-03-22', 245.75,1 , 'O', 978, 1),
(30, '2023-03-25', 315.00,0 , 'C', 979, 1),
(31, '2023-03-28', 205.50,1 , 'O', 980, 1),
(32, '2023-03-30', 275.00,0 , 'C', 981, 1),
(33, '2023-04-02', 340.75,1 , 'O', 982, 1),
(34, '2023-04-05', 315.20,0 , 'C', 983, 1),
(35, '2023-04-08', 290.00,1 , 'O', 984, 1),
(36, '2023-04-10', 360.45,0 , 'C', 985, 1),
(37, '2023-04-12', 240.75,1 , 'O', 986, 1),
(38, '2023-04-15', 275.50,0 , 'C', 987, 1),
(39, '2023-04-18', 205.00,1 , 'O', 988, 1),
(40, '2023-04-20', 315.75,0 , 'C', 989, 1),
(41, '2023-04-22', 325.00,1 , 'O', 990, 1),
(42, '2023-04-25', 360.20,0 , 'C', 991, 1),
(43, '2023-04-28', 250.75,1 , 'O', 992, 1),
(44, '2023-04-30', 305.45,0 , 'C', 993, 1),
(45, '2023-05-02', 340.50,1 , 'O', 994, 1),
(46, '2023-05-05', 275.00,0 , 'C', 995, 1),
(47, '2023-05-08', 295.75,1 , 'O', 996, 1),
(48, '2023-05-10', 365.20,0 , 'C', 997, 1),
(49, '2023-05-12', 255.00,1 , 'O', 998, 1),
(50, '2023-05-15', 300.45,0 , 'C', 999, 1),
(51, '2023-05-18', 320.75,1 , 'O', 1000, 1),
(52, '2023-05-20', 275.20,0 , 'C', 1001, 1),
(53, '2023-05-22', 290.00,1 , 'O', 1002, 1),
(54, '2023-05-25', 365.45,0 , 'C', 1003, 1),
(55, '2023-05-28', 245.75,1 , 'O', 1004, 1),
(56, '2023-05-30', 315.00,0 , 'C', 1005, 1),
(57, '2023-06-02', 305.50,1 , 'O', 1006, 1),
(58, '2023-06-05', 275.00,0 , 'C', 1007, 1),
(59, '2023-06-08', 295.75,1 , 'O', 1008, 1),
(60, '2023-06-10', 360.20,0 , 'C', 1009, 1),
(61, '2023-06-12', 245.00,1 , 'O', 1010, 1),
(62, '2023-06-15', 310.45,0 , 'C', 1011, 1),
(63, '2023-06-18', 340.75,1 , 'O', 1012, 1),
(64, '2023-06-20', 275.20,0 , 'C', 1013, 1),
(65, '2023-06-22', 295.00,1 , 'O', 1014, 1),
(66, '2023-06-25', 360.45,0 , 'C', 1015, 1),
(67, '2023-06-28', 255.75,1 , 'O', 1016, 1),
(68, '2023-06-30', 315.00,0 , 'C', 1017, 1),
(69, '2023-07-02', 320.50,1 , 'O', 1018, 1),
(70, '2023-07-05', 280.00,0 , 'C', 1019, 1),
(71, '2023-07-08', 300.75,1 , 'O', 1020, 1),
(72, '2023-07-10', 370.20,0 , 'C', 1021, 1),
(73, '2023-07-12', 260.00,1 , 'O', 1022, 1),
(74, '2023-07-15', 315.45,0 , 'C', 1023, 1),
(75, '2023-07-18', 350.75,1 , 'O', 1024, 1),
(76, '2023-07-20', 275.20,0 , 'C', 1025, 1),
(77, '2023-07-22', 295.00,1 , 'O', 1026, 1),
(78, '2023-07-25', 370.45,0 , 'C', 1027, 1),
(79, '2023-07-28', 260.75,1 , 'O', 1028, 1),
(80, '2023-07-30', 320.00,0 , 'C', 1029, 1),
(81, '2023-08-02', 330.50,1 , 'O', 1030, 1),
(82, '2023-08-05', 280.00,0 , 'C', 1031, 1),
(83, '2023-08-08', 310.75,1 , 'O', 1032, 1),
(84, '2023-08-10', 375.20,0 , 'C', 1033, 1),
(85, '2023-08-12', 265.00,1 , 'O', 1034, 1),
(86, '2023-08-15', 320.45,0 , 'C', 1035, 1),
(87, '2023-08-18', 355.75,1 , 'O', 1036, 1),
(88, '2023-08-20', 280.20,0 , 'C', 1037, 1),
(89, '2023-08-22', 300.00,1 , 'O', 1038, 1),
(90, '2023-08-25', 375.45,0 , 'C', 1039, 1),
(91, '2023-08-28', 270.75,1 , 'O', 1040, 1),
(92, '2023-08-30', 330.00,0 , 'C', 1041, 1),
(93, '2023-09-02', 335.50,1 , 'O', 1042, 1),
(94, '2023-09-05', 290.00,0 , 'C', 1043, 1),
(95, '2023-09-08', 315.75,1 , 'O', 1044, 1),
(96, '2023-09-10', 380.20,0 , 'C', 1045, 1),
(97, '2023-09-12', 270.00,1 , 'O', 1046, 1),
(98, '2023-09-15', 325.45,0 , 'C', 1047, 1),
(99, '2023-09-18', 360.75,1 , 'O', 1048, 1),
(100, '2023-09-20', 285.20,0 , 'C', 1049, 1),
(101, '2023-09-22', 305.00, 1 , 'O', 1050,1),
(102, '2023-09-25', 380.45, 0 , 'C', 959, 1),
(103, '2023-09-28', 275.75, 1 , 'O', 951, 1),
(104, '2023-09-30', 335.00, 0 , 'C', 952, 1),
(105, '2023-10-02', 340.50, 1 , 'O', 953, 1),
(106, '2023-10-05', 295.00, 0 , 'C', 954, 1),
(107, '2023-10-08', 320.75, 1 , 'O', 955, 1),
(108, '2023-10-10', 385.20, 0 , 'C', 956, 1),
(109, '2023-10-12', 275.00, 1 , 'O', 957, 1),
(110, '2023-10-15', 330.45, 0 , 'C', 958, 1),
(111, '2023-10-18', 365.75, 1 , 'O', 959, 1),
(112, '2023-10-20', 290.20, 0 , 'C', 960, 1),
(113, '2023-10-22', 310.00, 1 , 'O', 961, 1),
(114, '2023-10-25', 385.45, 0 , 'C', 962, 1),
(115, '2023-10-28', 280.75, 1 , 'O', 963, 1),
(116, '2023-10-30', 340.00, 0 , 'C', 964, 1),
(117, '2023-11-02', 345.50, 1 , 'O', 965, 1),
(118, '2023-11-05', 300.00, 0 , 'C', 966, 1),
(119, '2023-11-08', 325.75, 1 , 'O', 967, 1),
(120, '2023-11-10', 390.20, 0 , 'C', 968, 1),
(121, '2023-11-12', 280.00, 1 , 'O', 969, 1),
(122, '2023-11-15', 335.45, 0 , 'C', 970, 1),
(123, '2023-11-18', 370.75, 1 , 'O', 971, 1),
(124, '2023-11-20', 295.20, 0 , 'C', 972, 1),
(125, '2023-11-22', 315.00, 1 , 'O', 973, 1),
(126, '2023-11-25', 390.45, 0 , 'C', 974, 1),
(127, '2023-11-28', 285.75, 1 , 'O', 975, 1),
(128, '2023-11-30', 345.00, 0 , 'C', 976, 1),
(129, '2023-12-02', 350.50, 1 , 'O', 977, 1),
(130, '2023-12-05', 305.00, 0 , 'C', 978, 1);
SET IDENTITY_INSERT Invoice off; 
go

SET IDENTITY_INSERT Appointment on; 
go
-- Inserting 150 records into the Appointment table
INSERT INTO [dbo].[Appointment] (AppointmentID, AppointmentDate, AppointmentStatus, PatientID, ClinicID, receptionID)
VALUES
(1, '2023-07-01', 'C', 951, 1, 1),
(2, '2023-07-02', 'R', 952, 2, 1),
(3, '2023-07-03', 'S', 953, 3, 1),
(4, '2023-07-04', 'C', 954, 4, 1),
(5, '2023-07-05', 'R', 955, 5, 1),
(6, '2023-07-06', 'S', 956, 6, 1),
(7, '2023-07-07', 'C', 957, 7, 1),
(8, '2023-07-08', 'R', 958, 8, 1),
(9, '2023-07-09', 'S', 959, 9, 1),
(10, '2023-07-10', 'C', 960, 10, 1),
(11, '2023-07-11', 'R', 961, 11, 1),
(12, '2023-07-12', 'S', 962, 12, 1),
(13, '2023-07-13', 'C', 963, 13, 1),
(14, '2023-07-14', 'R', 964, 14, 1),
(15, '2023-07-15', 'S', 965, 15, 1),
(16, '2023-07-16', 'C', 966, 16, 1),
(17, '2023-07-17', 'R', 967, 17, 1),
(18, '2023-07-18', 'S', 968, 1, 1),
(19, '2023-07-19', 'C', 969, 2, 1),
(20, '2023-07-20', 'R', 970, 3, 1),
(21, '2023-07-21', 'S', 971, 4, 1),
(22, '2023-07-22', 'C', 972, 5, 1),
(23, '2023-07-23', 'R', 973, 6, 1),
(24, '2023-07-24', 'S', 974, 7, 1),
(25, '2023-07-25', 'C', 975, 8, 1),
(26, '2023-07-26', 'R', 976, 9, 1),
(27, '2023-07-27', 'S', 977, 10, 1),
(28, '2023-07-28', 'C', 978, 11, 1),
(29, '2023-07-29', 'R', 979, 12, 1),
(30, '2023-07-30', 'S', 980, 13, 1),
(31, '2023-07-31', 'C', 981, 14, 1),
(32, '2023-08-01', 'R', 982, 15, 1),
(33, '2023-08-02', 'S', 983, 16, 1),
(34, '2023-08-03', 'C', 984, 17, 1),
(35, '2023-08-04', 'R', 985, 1, 1),
(36, '2023-08-05', 'S', 986, 2, 1),
(37, '2023-08-06', 'C', 987, 3, 1),
(38, '2023-08-07', 'R', 988, 4, 1),
(39, '2023-08-08', 'S', 989, 5, 1),
(40, '2023-08-09', 'C', 990, 6, 1),
(41, '2023-08-10', 'R', 991, 7, 1),
(42, '2023-08-11', 'S', 992, 8, 1),
(43, '2023-08-12', 'C', 993, 9, 1),
(44, '2023-08-13', 'R', 994, 10, 1),
(45, '2023-08-14', 'S', 995, 11, 1),
(46, '2023-08-15', 'C', 996, 12, 1),
(47, '2023-08-16', 'R', 997, 13, 1),
(48, '2023-08-17', 'S', 998, 14, 1),
(49, '2023-08-18', 'C', 999, 15, 1),
(50, '2023-08-19', 'R', 1000, 16, 1),
(51, '2023-08-20', 'S', 1001, 17, 1),
(52, '2023-08-21', 'C', 1002, 1, 1),
(53, '2023-08-22', 'R', 1003, 2, 1),
(54, '2023-08-23', 'S', 1004, 3, 1),
(55, '2023-08-24', 'C', 1005, 4, 1),
(56, '2023-08-25', 'R', 1006, 5, 1),
(57, '2023-08-26', 'S', 1007, 6, 1),
(58, '2023-08-27', 'C', 1008, 7, 1),
(59, '2023-08-28', 'R', 1009, 8, 1),
(60, '2023-08-29', 'S', 1010, 9, 1),
(61, '2023-08-30', 'C', 1011, 10, 1),
(62, '2023-08-31', 'R', 1012, 11, 1),
(63, '2023-09-01', 'S', 1013, 12, 1),
(64, '2023-09-02', 'C', 1014, 13, 1),
(65, '2023-09-03', 'R', 1015, 14, 1),
(66, '2023-09-04', 'S', 1016, 15, 1),
(67, '2023-09-05', 'C', 1017, 16, 1),
(68, '2023-09-06', 'R', 1018, 17, 1),
(69, '2023-09-07', 'S', 1019, 1, 1),
(70, '2023-09-08', 'C', 1020, 2, 1),
(71, '2023-09-09', 'R', 1021, 3, 1),
(72, '2023-09-10', 'S', 1022, 4, 1),
(73, '2023-09-11', 'C', 1023, 5, 1),
(74, '2023-09-12', 'R', 1024, 6, 1),
(75, '2023-09-13', 'S', 1025, 7, 1),
(76, '2023-09-14', 'C', 1026, 8, 1),
(77, '2023-09-15', 'R', 1027, 9, 1),
(78, '2023-09-16', 'S', 1028, 10, 1),
(79, '2023-09-17', 'C', 1029, 11, 1),
(80, '2023-09-18', 'R', 1030, 12, 1),
(81, '2023-09-19', 'S', 1031, 13, 1),
(82, '2023-09-20', 'C', 1032, 14, 1),
(83, '2023-09-21', 'R', 1033, 15, 1),
(84, '2023-09-22', 'S', 1034, 16, 1),
(85, '2023-09-23', 'C', 1035, 17, 1),
(86, '2023-09-24', 'R', 1036, 1, 1),
(87, '2023-09-25', 'S', 1037, 2, 1),
(88, '2023-09-26', 'C', 1038, 3, 1),
(89, '2023-09-27', 'R', 1039, 4, 1),
(90, '2023-09-28', 'S', 1040, 5, 1),
(91, '2023-09-29', 'C', 1041, 6, 1),
(92, '2023-09-30', 'R', 1042, 7, 1),
(93, '2023-10-01', 'S', 1043, 8, 1),
(94, '2023-10-02', 'C', 1044, 9, 1),
(95, '2023-10-03', 'R', 1045, 10, 1),
(96, '2023-10-04', 'S', 1046, 11, 1),
(97, '2023-10-05', 'C', 1047, 12, 1),
(98, '2023-10-06', 'R', 1048, 13, 1),
(99, '2023-10-07', 'S', 1049, 14, 1),
(100, '2023-10-08', 'C', 1050, 15, 1),
(101, '2023-10-09', 'R', 951, 16, 1),
(102, '2023-10-10', 'S', 952, 17, 1),
(103, '2023-10-11', 'C', 953, 1, 1),
(104, '2023-10-12', 'R', 954, 2, 1),
(105, '2023-10-13', 'S', 955, 3, 1),
(106, '2023-10-14', 'C', 956, 4, 1),
(107, '2023-10-15', 'R', 957, 5, 1),
(108, '2023-10-16', 'S', 958, 6, 1),
(109, '2023-10-17', 'C', 959, 7, 1),
(110, '2023-10-18', 'R', 960, 8, 1),
(111, '2023-10-19', 'S', 961, 9, 1),
(112, '2023-10-20', 'C', 962, 10, 1),
(113, '2023-10-21', 'R', 963, 11, 1),
(114, '2023-10-22', 'S', 964, 12, 1),
(115, '2023-10-23', 'C', 965, 13, 1),
(116, '2023-10-24', 'R', 966, 14, 1),
(117, '2023-10-25', 'S', 967, 15, 1),
(118, '2023-10-26', 'C', 968, 16, 1),
(119, '2023-10-27', 'R', 969, 17, 1),
(120, '2023-10-28', 'S', 970, 1, 1),
(121, '2023-10-29', 'C', 971, 2, 1),
(122, '2023-10-30', 'R', 972, 3, 1),
(123, '2023-10-31', 'S', 973, 4, 1),
(124, '2023-11-01', 'C', 974, 5, 1),
(125, '2023-11-02', 'R', 975, 6, 1),
(126, '2023-11-03', 'S', 976, 7, 1),
(127, '2023-11-04', 'C', 977, 8, 1),
(128, '2023-11-05', 'R', 978, 9, 1),
(129, '2023-11-06', 'S', 979, 10, 1),
(130, '2023-11-07', 'C', 980, 11, 1),
(131, '2023-11-08', 'R', 981, 12, 1),
(132, '2023-11-09', 'S', 982, 13, 1),
(133, '2023-11-10', 'C', 983, 14, 1),
(134, '2023-11-11', 'R', 984, 15, 1),
(135, '2023-11-12', 'S', 985, 16, 1),
(136, '2023-11-13', 'C', 986, 17, 1),
(137, '2023-11-14', 'R', 987, 1, 1),
(138, '2023-11-15', 'S', 988, 2, 1),
(139, '2023-11-16', 'C', 989, 3, 1),
(140, '2023-11-17', 'R', 990, 4, 1),
(141, '2023-11-18', 'S', 991, 5, 1),
(142, '2023-11-19', 'C', 992, 6, 1),
(143, '2023-11-20', 'R', 993, 7, 1),
(144, '2023-11-21', 'S', 994, 8, 1),
(145, '2023-11-22', 'C', 995, 9, 1),
(146, '2023-11-23', 'R', 996, 10, 1),
(147, '2023-11-24', 'S', 997, 11, 1),
(148, '2023-11-25', 'C', 998, 12, 1),
(149, '2023-11-26', 'R', 999, 13, 1),
(150, '2023-11-27', 'S', 1000, 14, 1);
SET IDENTITY_INSERT Appointment off; 
go