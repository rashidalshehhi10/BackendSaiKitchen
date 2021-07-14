ALTER TABLE [File] NOCHECK CONSTRAINT all
ALTER TABLE [inquiry] NOCHECK CONSTRAINT all
ALTER TABLE [inquiryworkscope] NOCHECK CONSTRAINT all
ALTER TABLE [Measurement] NOCHECK CONSTRAINT all
ALTER TABLE [Design] NOCHECK CONSTRAINT all
ALTER TABLE [Quotation] NOCHECK CONSTRAINT all
ALTER TABLE [Customer] NOCHECK CONSTRAINT all
ALTER TABLE [Payment] NOCHECK CONSTRAINT all
ALTER TABLE [Notification] NOCHECK CONSTRAINT all
DELETE FROM [File]
DELETE FROM [inquiry]
DELETE FROM [inquiryworkscope]
DELETE FROM [Measurement]
DELETE FROM [Design]
DELETE FROM [Quotation]
DELETE FROM [Customer]
DELETE FROM [Payment]
DELETE FROM [Notification]
ALTER TABLE [File] CHECK CONSTRAINT all
ALTER TABLE [inquiry] CHECK CONSTRAINT all
ALTER TABLE [inquiryworkscope] CHECK CONSTRAINT all
ALTER TABLE [Measurement] CHECK CONSTRAINT all
ALTER TABLE [Design] CHECK CONSTRAINT all
ALTER TABLE [Quotation] CHECK CONSTRAINT all
ALTER TABLE [Customer] CHECK CONSTRAINT all
ALTER TABLE [Payment] CHECK CONSTRAINT all
ALTER TABLE [Notification] CHECK CONSTRAINT all