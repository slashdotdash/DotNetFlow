Given /^a user account has been registered$/ do
  @command = RegisterUserAccountCommand.new
  @command.fullname = Faker::Name.name
  @command.username = @username = @command.fullname.strip.gsub(/[^A-Za-z0-9]/, '')
  @command.email = Faker::Internet.email
  @command.password = "password"
  
  @command.post
end