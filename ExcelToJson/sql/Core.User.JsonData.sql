SELECT u.FirstName,
	u.LastName,
	u.Email EmailAddress,
	u.IsActive,
	isnull(u.DisplayUserId, '') DisplayUserId,
	isnull(u.WorkNumber, '') WorkNumber,
	isnull(u.MobileNumber, '') MobileNumber,
	isnull(u.SourceSystemId, '') SourceSystemId,
	isnull(u.SourceSystemName, '') SourceSystemName,
	isnull(v.SourceSystemId, '') VendorCode,
	isnull(e.EmployeeDisplay, '') EmployeeId,
	isnull(pDate.PreferenceValue, '') "DateFormat",
	isnull(pDecimal.PreferenceValue, '') DecimalFormat,
	isnull(pLanguage.PreferenceValue, '') "Language",
	isnull(u.StartDate, '') StartDate,
	isnull(u.enddate, '') EndDate
FROM i8core.users u
LEFT OUTER JOIN i8core.Vendor v ON u.VendorId = v.VendorId
LEFT OUTER JOIN i8core.Employee e ON u.EmployeeId = e.EmployeeId
LEFT OUTER JOIN i8core.UserPreference pDate ON (
		u.UserId = pdate.UserId
		AND pDate.PreferencePath = 'DateFormat'
		)
LEFT OUTER JOIN i8core.UserPreference pDecimal ON (
		u.UserId = pdate.UserId
		AND pDate.PreferencePath = 'NumberFormat'
		)
LEFT OUTER JOIN i8core.UserPreference pLanguage ON (
		u.UserId = pdate.UserId
		AND pDate.PreferencePath = 'Language'
		);
