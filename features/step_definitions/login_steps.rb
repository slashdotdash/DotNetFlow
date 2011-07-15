When /^I enter my username and password$/ do
  @username = @command.username
  @password = @command.password
  
  When %{I fill in "Username or E-mail" with "#{@username}"}
  When %{I fill in "Password" with "#{@password}"}
end