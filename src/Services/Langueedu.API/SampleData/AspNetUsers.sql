BEGIN;

INSERT INTO AspNetUsers(Id, LanguageCode, IsActive, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES('1111-1111-1111-1111', 'tr_TR', 0, 'witcherfearless', 'WITCHERFEARLESS', 'aldemirali93@gmail.com', 'ALDEMIRALI93@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEAcWHqgDCCt4ZiujO05dzPeyPzTH7NmIMi0k52kIkWkx3pCTb5fRNzBcgnJ31YLo3g==', 'e583614a-b37c-4dbb-99f3-3cda6f92fd53', '7fa6247c-e333-4fa7-a51e-7beeaa183d47', '5444261154', 0, 0, NULL, 0, 0);
INSERT INTO AspNetUsers(Id, LanguageCode, IsActive, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES('2222-2222-2222-2222', 'tr_TR', 0, 'demo', 'DEMO', 'demo@demo.com', 'DEMO@DEMO.COM', 0, 'AQAAAAEAACcQAAAAEMxHVMGugBbcgoL68Hif3FhXfa5gHdNZ1ihK+sV1LCc/IV9wIhflnMjfW80MbAR5MA==', '115d0ea3-63da-4adb-89de-67708ade1083', '43efdfe0-abb9-4b05-bafb-b9623f97e5bf', '0000000000', 0, 0, NULL, 0, 0);
INSERT INTO AspNetUsers(Id, LanguageCode, IsActive, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES('3333-3333-3333-bot', 'en_US', 0, 'guest123', 'GUEST123', 'guest@guest.com', 'GUEST@GUEST.COM', 0, 'AQAAAAEAACcQAAAAEH4pwN7y9OfZb9jVlWMjBbzS18+GRgREnAo5Q7S623c9NIqj7oaNqwFXRb6KoOF5tA==', '99694af1-9a61-4ed0-9ada-7502baf1d883', '434138ad-db4f-46ae-b93d-7d1e5e11035e', '1234567890', 0, 0, NULL, 0, 0);

COMMIT;