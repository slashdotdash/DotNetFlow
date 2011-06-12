module NavigationHelpers
  # Maps a name to a path. Used by the
  #
  #   When /^I go to (.+)$/ do |page_name|
  #
  # step definition in web_steps.rb
  #
  def path_to(page_name)
    case page_name
    
	when /home page/
      '/'
	  
    when /submit item page/
      '/submit'

	when /registration page/
      '/register'

	when /login page/
	  '/login'
	
	when /logout page/
	  '/logout'
	
	# Admin area
	
	when /submissions pending approval page/
		'/admin/pending'
	
    else
      raise "Can't find mapping from \"#{page_name}\" to a path.\n" +
        "Now, go and add a mapping in #{__FILE__}"
    end
  end
end

World(NavigationHelpers)