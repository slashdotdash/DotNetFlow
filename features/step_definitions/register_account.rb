When /^I complete the required fields for registration$/ do
  When %{I fill in "Full Name" with "Ben Smith"}
  When %{I fill in "E-mail" with "ben@dotnetflow.com"}
  When %{I fill in "Password" with "password"}
end

Then /^I should be logged in$/ do
  pending # express the regexp above with the code you wish you had
end