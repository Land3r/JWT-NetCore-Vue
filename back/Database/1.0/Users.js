db = connect("localhost:27017/ControlPanelDb")
db.Users.insertOne({
	_id: UUID(),
	FirstName: "test",
	LastName: "test",
	Username: "test",
	Email: "test@yopmail.com",
	Password: "041e66a2acaf543611ee5a8f028f2689de3b2cd22f83233968486fb968a865c5483f963901294ff4efb4eda27dbf527fe62a0b7e7e69041b5707e3e061140432"
});