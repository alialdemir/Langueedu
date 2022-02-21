BEGIN;

INSERT INTO AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp) VALUES('admin', 'Admin', 'ADMIN', 'bafec1c7-f239-4e75-8cc5-b88565b3b572');
INSERT INTO AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp) VALUES('user', 'User', 'USER', '00ed3cf0-f638-4dd5-ba9f-a5924892c947');

COMMIT;