require 'bcrypt'
require 'httparty'
require 'json'
require 'guid'

class RegisterUserAccountCommand
	include BCrypt
	include HTTParty
	
  	base_uri 'http://localhost:3000/'
  	#debug_output $stdout
  	#default_params :output => 'json'
  	format :json
  	
	attr_accessor :user_id
	attr_accessor :fullname
	attr_accessor :username
	attr_accessor :email
	attr_accessor :password
	attr_accessor :password_hash
	attr_accessor :website
	
	def initialize
      @user_id = Guid.new.to_s
	end

    def password=(new_password)
      @password = new_password
      @password_hash = Password.create(new_password)
    end
    
    def to_json
      JSON.dump({ 
    	'FullName' => @fullname, 
    	'UserId' => @user_id,
    	'Username' => @username,
    	'Email' => @email,
    	'Password' => @password_hash,
    	'Website' => @website,
    	'Twitter' => @twitter
	  })
	end
    
    def post
    	options = { :body => self.to_json }
    	pp options
    	pp self.class.post("/admin/command/#{self.class.name}", options)
	end
end